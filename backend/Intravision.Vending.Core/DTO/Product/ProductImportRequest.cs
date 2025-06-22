namespace Intravision.Vending.Core.DTO.Product;

/// <summary>
/// DTO-запрос на импорт товара из внешнего источника (например, Excel).
/// </summary>
/// <param name="Name">Название товара (может быть null).</param>
/// <param name="Price">Цена товара.</param>
/// <param name="Quantity">Количество на складе.</param>
/// <param name="Brand">Название бренда (может быть null).</param>
/// <param name="ImageUrl">Ссылка на изображение товара (может быть null).</param>
public record ProductImportRequest(
    string? Name,
    int Price,
    int Quantity,
    string? Brand,
    string? ImageUrl
);