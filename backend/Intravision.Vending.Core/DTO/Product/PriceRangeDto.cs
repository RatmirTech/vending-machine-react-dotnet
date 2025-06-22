namespace Intravision.Vending.Core.DTO.Product;

/// <summary>
/// DTO, представляющий диапазон цен товаров.
/// </summary>
/// <param name="MinPrice">Минимальная цена среди товаров.</param>
/// <param name="MaxPrice">Максимальная цена среди товаров.</param>
public record PriceRangeDto(int MinPrice, int MaxPrice);