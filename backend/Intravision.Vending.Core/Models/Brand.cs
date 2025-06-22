namespace Intravision.Vending.Core.Models;

/// <summary>
/// Представляет бренд товара.
/// </summary>
public class Brand
{
    /// <summary>
    /// Уникальный идентификатор бренда.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название бренда.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Коллекция товаров, относящихся к данному бренду.
    /// </summary>
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
