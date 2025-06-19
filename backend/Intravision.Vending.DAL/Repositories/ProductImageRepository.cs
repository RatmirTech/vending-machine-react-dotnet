using Intravision.Vending.Core.Abstractions.Repositories;
using Intravision.Vending.Core.Models;
using Intravision.Vending.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace Intravision.Vending.DAL.Repositories;

public class ProductImageRepository : Repository<ProductImage, Guid>, IProductImageRepository
{
    public ProductImageRepository(EfContext context)
        : base(context)
    {

    }

    public async Task<IEnumerable<ProductImage>> GetByProductIdAsync(Guid productId, CancellationToken token)
    {
        return await _dbSet.Where(x => x.ProductId == productId).ToListAsync(token);
    }
}