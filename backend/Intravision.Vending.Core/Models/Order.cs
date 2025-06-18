namespace Intravision.Vending.Core.Models;

public class Order
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int TotalPrice { get; set; }

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
