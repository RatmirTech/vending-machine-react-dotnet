namespace Intravision.Vending.Core.DTO.Product;

/// <summary>
/// DTO, представляющий данные о товаре для выдачи на клиент.
/// </summary>
public class ProductGetResponse
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
    /// Цена товара в условных единицах
    /// </summary>
    public int Price { get; set; }

    /// <summary>
    /// Количество товара на складе.
    /// </summary>
    public int QuantityInStock { get; set; }

    /// <summary>
    /// Коллекция изображений товара (может быть null).
    /// </summary>
    public IEnumerable<ProductImageGetResponse>? Images { get; set; }
}
