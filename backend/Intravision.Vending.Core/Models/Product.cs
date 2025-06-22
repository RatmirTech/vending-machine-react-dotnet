namespace Intravision.Vending.Core.Models;

/// <summary>
/// Представляет товар в системе (автомате, складе и т.д.).
/// </summary>
public class Product
{
    /// <summary>
    /// Уникальный идентификатор товара.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название товара.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Цена товара.
    /// </summary>
    public int Price { get; set; }

    /// <summary>
    /// Количество товара в наличии.
    /// </summary>
    public int QuantityInStock { get; set; }

    /// <summary>
    /// Идентификатор бренда, к которому принадлежит товар.
    /// </summary>
    public Guid BrandId { get; set; }

    /// <summary>
    /// Навигационное свойство к бренду.
    /// </summary>
    public Brand? Brand { get; set; }

    /// <summary>
    /// Коллекция изображений товара.
    /// </summary>
    public ICollection<ProductImage>? Images { get; set; }
}

