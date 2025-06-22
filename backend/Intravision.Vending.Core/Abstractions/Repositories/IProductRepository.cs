using Intravision.Vending.Core.Abstractions;
using Intravision.Vending.Core.DTO.Product;
using Intravision.Vending.Core.Models;

/// <summary>
/// Интерфейс репозитория для доступа и управления данными о напитках
/// </summary>
public interface IProductRepository : IRepository<Product, Guid>
{
    /// <summary>
    /// Асинхронно получает список продуктов, отфильтрованных по бренду и диапазону цен
    /// </summary>
    /// <param name="brandId">Необязательный идентификатор бренда для фильтрации</param>
    /// <param name="minPrice">Необязательная минимальная цена</param>
    /// <param name="maxPrice">Необязательная максимальная цена</param>
    /// <param name="token">Токен отмены</param>
    /// <returns>
    /// Задача, представляющая асинхронную операцию.
    /// Результатом является коллекция объектов <see cref="Product"/>, соответствующих фильтру
    /// </returns>
    Task<IEnumerable<Product>> GetFilteredAsync(
        Guid? brandId,
        int? minPrice,
        int? maxPrice,
        CancellationToken token = default);

    /// <summary>
    /// Асинхронно получает минимальное и максимальное значения цен для продуктов, 
    /// с возможностью фильтрации по бренду
    /// </summary>
    /// <param name="brandId">Необязательный идентификатор бренда для фильтрации</param>
    /// <param name="token">Токен отмены</param>
    /// <returns>
    /// Задача, представляющая асинхронную операцию
    /// Результатом является объект <see cref="PriceRangeDto"/> с минимальной и максимальной ценой
    /// </returns>
    Task<PriceRangeDto> GetPriceRangeAsync(
        Guid? brandId,
        CancellationToken token = default);
}
