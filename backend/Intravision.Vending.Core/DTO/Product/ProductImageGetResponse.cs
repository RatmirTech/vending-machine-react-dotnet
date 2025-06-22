namespace Intravision.Vending.Core.DTO.Product;

/// <summary>
/// DTO, представляющий изображение товара.
/// </summary>
public class ProductImageGetResponse
{
    /// <summary>
    /// URL-адрес изображения товара.
    /// </summary>
    public string ImageUrl { get; set; } = string.Empty;
}