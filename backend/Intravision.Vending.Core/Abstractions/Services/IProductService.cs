using Intravision.Vending.Core.Models;

namespace Intravision.Vending.Core.Abstractions.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetProducts(
        Guid? brandId,
        int? minPriceInCents,
        int? maxPriceInCents,
        CancellationToken token = default);
}
