namespace Intravision.Vending.Core.Models;

/// <summary>
/// Представляет заказ, включающий дату, стоимость и список товаров.
/// </summary>
public class Order
{
    /// <summary>
    /// Уникальный идентификатор заказа.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Дата и время создания заказа (UTC).
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Общая сумма заказа.
    /// </summary>
    public int TotalPrice { get; set; }

    /// <summary>
    /// Список позиций (товаров) в заказе.
    /// </summary>
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}

