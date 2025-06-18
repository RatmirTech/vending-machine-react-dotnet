using Intravision.Vending.Core.Models;

namespace Intravision.Vending.Core.Abstractions.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetFilteredAsync(
    Guid? brandId,
    int? minPrice,
    int? maxPrice,
    CancellationToken token = default);

    Task<PriceRangeDto> GetPriceRangeAsync(
        Guid? brandId,
        CancellationToken token = default);
}

public record PriceRangeDto(int MinPrice, int MaxPrice);