using Intravision.Vending.Core.Abstractions.Repositories;
using Intravision.Vending.Core.DTO.Product;
using Intravision.Vending.Core.Models;
using Intravision.Vending.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace Intravision.Vending.DAL.Repositories;

public class ProductRepository : Repository<Product, Guid>, IProductRepository
{
    public ProductRepository(EfContext context)
        : base(context)
    {

    }

    public async Task<IEnumerable<Product>> GetFilteredAsync(
        Guid? brandId,
        int? minPrice,
        int? maxPrice,
        CancellationToken token = default)
    {
        var q = _dbSet.AsQueryable();

        if (brandId.HasValue)
        {
            q = q.Where(p => p.BrandId == brandId.Value);
        }

        if (minPrice.HasValue)
        {
            q = q.Where(p => p.Price >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            q = q.Where(p => p.Price <= maxPrice.Value);
        }

        return await q.ToListAsync();
    }

    public async Task<PriceRangeDto> GetPriceRangeAsync(
        Guid? brandId,
        CancellationToken token = default)
    {
        var q = _dbSet.AsQueryable();
        if (brandId.HasValue)
        {
            q = q.Where(p => p.BrandId == brandId.Value);
        }

        if (!q.Any())
        {
            int max = _dbSet.Max(p => p.Price);
            return new PriceRangeDto(0, max);
        }

        int minPrice = await q.MinAsync(p => p.Price, token);
        int maxPrice = await q.MaxAsync(p => p.Price, token);

        return new PriceRangeDto(minPrice, maxPrice);
    }
}