namespace Intravision.Vending.Core.Models;

public class OrderItem
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public string ProductName { get; set; } = string.Empty;

    public string BrandName { get; set; } = string.Empty;

    public int UnitPrice { get; set; }

    public int Quantity { get; set; }

    public int TotalPrice => Quantity * UnitPrice;
}
