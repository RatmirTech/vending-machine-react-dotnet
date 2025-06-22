namespace Intravision.Vending.Core.DTO.Order;

/// <summary>
/// Дто напитка из заказа
/// </summary>
public class OrderItemDto
{
    /// <summary>
    /// Идентификатор продукта
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Количество продукта
    /// </summary>
    public int Quantity { get; set; }
}