namespace Intravision.Vending.Core.Models;

/// <summary>
/// Представляет изображение, связанное с товаром.
/// </summary>
public class ProductImage
{
    /// <summary>
    /// Уникальный идентификатор изображения.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// URL-адрес изображения.
    /// </summary>
    public string ImageUrl { get; set; } = string.Empty;

    /// <summary>
    /// Идентификатор товара, к которому относится изображение.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Навигационное свойство к товару.
    /// </summary>
    public Product? Product { get; set; }
}
