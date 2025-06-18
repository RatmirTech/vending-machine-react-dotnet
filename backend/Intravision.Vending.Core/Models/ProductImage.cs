namespace Intravision.Vending.Core.Models;

public class ProductImage
{
    public Guid Id { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
}