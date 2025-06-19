using AutoMapper;
using Intravision.Vending.Core.Abstractions.Repositories;
using Intravision.Vending.Core.Abstractions.Services;
using Intravision.Vending.Core.DTO.Order;
using Intravision.Vending.Core.Models;
using Microsoft.Extensions.Logging;

namespace Intravision.Vending.Core.Services;

public class OrderService : IOrderService
{
    private readonly ILogger<OrderService> _logger;

    private readonly IOrderRepository _orderRepository;

    private readonly IOrderItemRepository _orderItemRepository;

    private readonly IProductRepository _productRepository;

    private readonly ICoinRepository _coinRepository;

    private readonly IBrandRepository _brandRepository;

    private readonly IMapper _mapper;

    public OrderService(
        ILogger<OrderService> logger,
        IOrderRepository order,
        IOrderItemRepository item,
        IMapper mapper,
        IProductRepository productRepository,
        ICoinRepository coinRepository,
        IBrandRepository brandRepository)
    {
        _logger = logger;
        _orderRepository = order;
        _orderItemRepository = item;
        _mapper = mapper;
        _productRepository = productRepository;
        _coinRepository = coinRepository;
        _brandRepository = brandRepository;
    }

    public async Task<OrderCreateResponse> CreateOrder(
        OrderCreateRequest request,
        CancellationToken token = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var productIds = request.Items.Select(i => i.ProductId).ToList();

        // все товары которые выбраны для заказа
        var products = await _productRepository.FindAsync(p => productIds.Contains(p.Id), token);

        var productMap = products.ToDictionary(p => p.Id);


        var order = new Order
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            TotalPrice = 0
        };

        await _orderRepository.CreateAsync(order, token);

        // проверка наличия товаров и подсчёт общей стоимости заказа
        foreach (var item in request.Items)
        {
            if (!productMap.TryGetValue(item.ProductId, out var product))
                throw new ArgumentNullException(nameof(item.ProductId));

            var brand = await _brandRepository.GetByIdAsync(product.BrandId, token);

            var orderItem = new OrderItem
            {
                BrandName = brand?.Name ?? string.Empty,
                ProductName = product.Name,
                OrderId = order.Id,
                Quantity = item.Quantity,
                UnitPrice = product.Price
            };

            order.TotalPrice += orderItem.Quantity * orderItem.UnitPrice * 100;
            await _orderItemRepository.CreateAsync(orderItem, token);
        }

        // сумма внесённых монет
        int insertedTotal = request.InsertedCoins.Sum(c => c.Key * c.Value * 100);

        if (insertedTotal < order.TotalPrice)
            throw new Exception("Недостаточно средств.");

        var allCoins = (await _coinRepository.GetAllAsync(token)).ToList();

        //добавить монеты пользователя в автомат
        foreach (var (denomination, quantityToAdd) in request.InsertedCoins)
        {
            int d = denomination * 100;
            Coin? coin = allCoins.FirstOrDefault(c => c.Denomination == d);
            if (coin is null)
                throw new ArgumentNullException(nameof(coin));

            coin.Quantity += quantityToAdd;
            await _coinRepository.UpdateAsync(coin, token);
        }

        #region[Проверка возможности выдачи сдачи]
        // сдача
        int changeAmount = insertedTotal - order.TotalPrice;

        Dictionary<int, int> changeToGive = new();

        if (changeAmount > 0)
        {
            try
            {
                changeToGive = CalculateChange(changeAmount, allCoins);
            }
            catch (Exception ex)
            {
                return new OrderCreateResponse()
                {
                    ChangeAmount = 0,
                    Success = false,
                    Message = "Извините, в данный момент мы не можем продать вам товар по причине того, что автомат не может выдать вам нужную сдачу"
                };
            }

            foreach (var (denomination, qty) in changeToGive)
            {
                var coin = allCoins.First(c => c.Denomination == denomination);
                coin.Quantity -= qty;
                await _coinRepository.UpdateAsync(coin, token);
            }
        }

        await _coinRepository.SaveChangesAsync(token);
        await _orderRepository.SaveChangesAsync(token);
        await _orderItemRepository.SaveChangesAsync(token);

        return new OrderCreateResponse()
        {
            ChangeAmount = changeAmount / 100,
            Success = true,
            Message = changeAmount > 0
                ? $"Ваш заказ принят. Сдача: {changeAmount / 100} руб."
                : "Ваш заказ принят. Сдача не требуется."
        };
        #endregion
    }

    private static Dictionary<int, int> CalculateChange(
        int changeAmount,
        List<Coin> availableCoins)
    {
        var result = new Dictionary<int, int>();

        Span<Coin> coins = availableCoins.ToArray().AsSpan();
        coins.Sort((a, b) => b.Denomination.CompareTo(a.Denomination));

        foreach (ref readonly var coin in coins)
        {
            int coinValue = coin.Denomination;
            if (coinValue == 0 || changeAmount < coinValue) continue;

            int maxUsable = Math.Min(changeAmount / coinValue, coin.Quantity);
            if (maxUsable == 0) continue;

            result[coinValue] = maxUsable;
            changeAmount -= maxUsable * coinValue;

            if (changeAmount == 0) return result;
        }

        throw new Exception();
    }
}
