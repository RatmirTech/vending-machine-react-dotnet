namespace Intravision.Vending.Core.DTO.Order;

/// <summary>
/// Запрос на создание заказа
/// </summary>
public class OrderCreateRequest
{
    /// <summary>
    /// Список выбранных товаров и их количества
    /// </summary>
    public List<OrderItemDto> Items { get; set; } = new();

    /// <summary>
    /// Внесенные монеты: ключ — номинал, значение — количество
    /// </summary>
    public Dictionary<int, int> InsertedCoins { get; set; } = new();
}