namespace Intravision.Vending.Core.DTO.Order;

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