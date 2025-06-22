using AutoMapper;
using Intravision.Vending.Core.Abstractions.Repositories;
using Intravision.Vending.Core.Abstractions.Services;
using Intravision.Vending.Core.DTO.Order;
using Intravision.Vending.Core.Models;
using Microsoft.Extensions.Logging;

namespace Intravision.Vending.Core.Services;

/// <summary>
/// Сервис для обработки заказов.
/// Реализует бизнес-логику создания заказов, работы с товарами, монетами и брендами.
/// </summary>
public class OrderService : IOrderService
{
    /// <summary>
    /// Логгер для записи действий и ошибок сервиса заказов.
    /// </summary>
    private readonly ILogger<OrderService> _logger;

    /// <summary>
    /// Репозиторий для управления заказами.
    /// </summary>
    private readonly IOrderRepository _orderRepository;

    /// <summary>
    /// Репозиторий для управления позициями заказов.
    /// </summary>
    private readonly IOrderItemRepository _orderItemRepository;

    /// <summary>
    /// Репозиторий для доступа к товарам.
    /// </summary>
    private readonly IProductRepository _productRepository;

    /// <summary>
    /// Репозиторий для управления монетами (используется для расчёта сдачи).
    /// </summary>
    private readonly ICoinRepository _coinRepository;

    /// <summary>
    /// Репозиторий для работы с брендами товаров.
    /// </summary>
    private readonly IBrandRepository _brandRepository;

    /// <summary>
    /// Маппер для преобразования DTO и сущностей.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Создаёт экземпляр сервиса заказов.
    /// </summary>
    /// <param name="logger">Логгер для логирования операций сервиса.</param>
    /// <param name="order">Репозиторий заказов.</param>
    /// <param name="item">Репозиторий позиций заказов.</param>
    /// <param name="mapper">AutoMapper для преобразования моделей.</param>
    /// <param name="productRepository">Репозиторий товаров.</param>
    /// <param name="coinRepository">Репозиторий монет для расчёта сдачи.</param>
    /// <param name="brandRepository">Репозиторий брендов.</param>
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
        ValidateRequest(request);

        var productMap = await LoadProductsAsync(request.Items, token);
        var order = await InitializeOrderAsync(token);

        await AddItemsToOrderAsync(order, request.Items, productMap, token);
        int totalPrice = order.TotalPrice;

        int insertedTotal = CalculateInsertedTotal(request.InsertedCoins);
        EnsureSufficientFunds(insertedTotal, totalPrice);

        var allCoins = (await _coinRepository.GetAllAsync(token)).ToList();
        await AddInsertedCoinsAsync(request.InsertedCoins, allCoins, token);

        int changeAmount = insertedTotal - totalPrice;
        var changeToGive = await TryDispenseChangeAsync(changeAmount, allCoins, token);

        await SaveAllAsync(token);
        return BuildResponse(changeAmount, changeToGive);
    }

    private void ValidateRequest(OrderCreateRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));
    }

    private async Task<Dictionary<Guid, Product>> LoadProductsAsync(
        IEnumerable<OrderItemDto> items,
        CancellationToken token)
    {
        var ids = items.Select(i => i.ProductId).ToList();
        var products = await _productRepository.FindAsync(p => ids.Contains(p.Id), token);
        return products.ToDictionary(p => p.Id);
    }

    private async Task<Order> InitializeOrderAsync(CancellationToken token)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            TotalPrice = 0
        };
        await _orderRepository.CreateAsync(order, token);
        return order;
    }

    private async Task AddItemsToOrderAsync(
        Order order,
        IEnumerable<OrderItemDto> items,
        Dictionary<Guid, Product> productMap,
        CancellationToken token)
    {
        foreach (var item in items)
        {
            if (!productMap.TryGetValue(item.ProductId, out var product))
                throw new ArgumentNullException(nameof(item.ProductId));

            var brand = await _brandRepository.GetByIdAsync(product.BrandId, token);
            var orderItem = CreateOrderItem(order.Id, item.Quantity, product, brand?.Name);

            order.TotalPrice += orderItem.Quantity * orderItem.UnitPrice * 100;
            await _orderItemRepository.CreateAsync(orderItem, token);
        }
    }

    private OrderItem CreateOrderItem(
        Guid orderId,
        int quantity,
        Product product,
        string brandName)
    {
        return new OrderItem
        {
            OrderId = orderId,
            Quantity = quantity,
            UnitPrice = product.Price,
            ProductName = product.Name,
            BrandName = brandName ?? string.Empty
        };
    }

    private int CalculateInsertedTotal(
        IDictionary<int, int> insertedCoins)
    {
        return insertedCoins.Sum(c => c.Key * c.Value * 100);
    }

    private void EnsureSufficientFunds(int inserted, int price)
    {
        if (inserted < price)
            throw new InvalidOperationException("Недостаточно средств.");
    }

    private async Task AddInsertedCoinsAsync(
        IDictionary<int, int> insertedCoins,
        List<Coin> allCoins,
        CancellationToken token)
    {
        foreach (var (denom, qty) in insertedCoins)
        {
            int d = denom * 100;
            var coin = allCoins.FirstOrDefault(c => c.Denomination == d)
                       ?? throw new ArgumentNullException(nameof(denom));

            coin.Quantity += qty;
            await _coinRepository.UpdateAsync(coin, token);
        }
    }

    private async Task<Dictionary<int, int>> TryDispenseChangeAsync(
        int changeAmount,
        List<Coin> allCoins,
        CancellationToken token)
    {
        if (changeAmount <= 0)
            return new Dictionary<int, int>();

        Dictionary<int, int> change;
        try
        {
            change = CalculateChange(changeAmount, allCoins);
        }
        catch
        {
            return new Dictionary<int, int>();
        }

        foreach (var (denom, qty) in change.Where(x => x.Value > 0))
        {
            var coin = allCoins.First(c => c.Denomination == denom);
            coin.Quantity -= qty;
            await _coinRepository.UpdateAsync(coin, token);
        }

        return change;
    }

    private async Task SaveAllAsync(CancellationToken token)
    {
        await _coinRepository.SaveChangesAsync(token);
        await _orderRepository.SaveChangesAsync(token);
        await _orderItemRepository.SaveChangesAsync(token);
    }

    private OrderCreateResponse BuildResponse(
        int changeAmount,
        Dictionary<int, int> changeToGive)
    {
        return new OrderCreateResponse
        {
            ChangeAmount = changeAmount / 100,
            ChangeToGive = changeToGive.ToDictionary(k => k.Key / 100, v => v.Value),
            Success = true,
            Message = changeAmount > 0
                ? $"Ваш заказ принят. Сдача: {changeAmount / 100} руб."
                : "Ваш заказ принят. Сдача не требуется."
        };
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
            if (coinValue == 0)
            {
                result[coinValue] = 0;
                continue;
            }

            int maxUsable = 0;

            if (changeAmount >= coinValue)
            {
                maxUsable = Math.Min(changeAmount / coinValue, coin.Quantity);
                changeAmount -= maxUsable * coinValue;
            }

            result[coinValue] = maxUsable;

            if (changeAmount == 0)
                continue;
        }

        if (changeAmount > 0)
        {
            throw new Exception();
        }

        return result;
    }

}
