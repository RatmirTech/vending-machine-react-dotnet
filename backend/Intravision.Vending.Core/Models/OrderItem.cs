namespace Intravision.Vending.Core.Models;

/// <summary>
/// Представляет товарную позицию в заказе.
/// </summary>
public class OrderItem
{
    /// <summary>
    /// Уникальный идентификатор позиции.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор заказа, к которому принадлежит позиция.
    /// </summary>
    public Guid OrderId { get; set; }

    /// <summary>
    /// Навигационное свойство к заказу.
    /// </summary>
    public Order Order { get; set; } = null!;

    /// <summary>
    /// Название товара.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Название бренда товара.
    /// </summary>
    public string BrandName { get; set; } = string.Empty;

    /// <summary>
    /// Цена за единицу товара.
    /// </summary>
    public int UnitPrice { get; set; }

    /// <summary>
    /// Количество товара в позиции.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Общая стоимость позиции (кол-во * цена).
    /// </summary>
    public int TotalPrice => Quantity * UnitPrice;
}

