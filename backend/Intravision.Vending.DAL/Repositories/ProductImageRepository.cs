using Intravision.Vending.Core.Abstractions.Repositories;
using Intravision.Vending.Core.Models;
using Intravision.Vending.DAL.Context;

namespace Intravision.Vending.DAL.Repositories;

public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
{
    public ProductImageRepository(EfContext context)
        : base(context)
    {

    }
}
