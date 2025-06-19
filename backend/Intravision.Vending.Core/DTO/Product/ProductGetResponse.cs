namespace Intravision.Vending.Core.DTO.Product;

public class ProductGetResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Price { get; set; }

    public int QuantityInStock { get; set; }

    public IEnumerable<ProductImageGetResponse>? Images { get; set; }
}