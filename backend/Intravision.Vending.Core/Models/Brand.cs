namespace Intravision.Vending.Core.Models;

public class Brand
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
