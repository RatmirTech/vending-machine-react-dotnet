using Intravision.Vending.Core.Models;

namespace Intravision.Vending.Core.Abstractions.Repositories;

public interface IProductImageRepository : IRepository<ProductImage, Guid>
{
    Task<IEnumerable<ProductImage>> GetByProductIdAsync(Guid productId, CancellationToken token = default);
}
