namespace Intravision.Vending.Core.Models;

public class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Price { get; set; }

    public int QuantityInStock { get; set; }

    public Guid BrandId { get; set; }
    public Brand? Brand { get; set; }

    public ICollection<ProductImage>? Images { get; set; }
}
