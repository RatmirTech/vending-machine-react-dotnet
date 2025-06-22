using Intravision.Vending.Core.DTO.Product;

namespace Intravision.Vending.Core.Abstractions.Services;

/// <summary>
/// Сервис для управления товарами и получения информации о них.
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Асинхронно получает список товаров с возможной фильтрацией по бренду и диапазону цен.
    /// </summary>
    /// <param name="brandId">Необязательный идентификатор бренда.</param>
    /// <param name="minPrice">Необязательная минимальная цена.</param>
    /// <param name="maxPrice">Необязательная максимальная цена.</param>
    /// <param name="token">Токен отмены для прерывания асинхронной операции.</param>
    /// <returns>
    /// Задача, результатом которой является коллекция <see cref="ProductGetResponse"/> с информацией о товарах.
    /// </returns>
    Task<IEnumerable<ProductGetResponse>> GetProducts(
        Guid? brandId,
        int? minPrice,
        int? maxPrice,
        CancellationToken token = default);

    /// <summary>
    /// Асинхронно получает минимальное и максимальное значение цен товаров, 
    /// опционально с фильтрацией по бренду.
    /// </summary>
    /// <param name="brandId">Необязательный идентификатор бренда.</param>
    /// <param name="token">Токен отмены для прерывания асинхронной операции.</param>
    /// <returns>
    /// Задача, результатом которой является объект <see cref="PriceRangeDto"/> с диапазоном цен.
    /// </returns>
    Task<PriceRangeDto> GetPriceRange(
        Guid? brandId,
        CancellationToken token = default);

    /// <summary>
    /// Импортирует данные о товарах из Excel-файла.
    /// </summary>
    /// <param name="fileStream">Поток файла Excel.</param>
    /// <param name="token">Токен отмены для прерывания асинхронной операции.</param>
    /// <returns>
    /// Задача, результатом которой является логическое значение: 
    /// <c>true</c>, если импорт прошёл успешно, иначе — <c>false</c>.
    /// </returns>
    Task<bool> ImportFromExcel(Stream fileStream, CancellationToken token = default);
}