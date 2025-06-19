using Intravision.Vending.Core.DTO.Product;
using Intravision.Vending.Core.Models;

namespace Intravision.Vending.Core.Abstractions.Repositories;

public interface IProductRepository : IRepository<Product, Guid>
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