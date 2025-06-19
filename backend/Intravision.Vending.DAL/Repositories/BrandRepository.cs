using Intravision.Vending.Core.Abstractions.Repositories;
using Intravision.Vending.Core.Models;
using Intravision.Vending.DAL.Context;

namespace Intravision.Vending.DAL.Repositories;

public class BrandRepository : Repository<Brand, Guid>, IBrandRepository
{
    public BrandRepository(EfContext context)
        : base(context)
    {

    }
}
