using Intravision.Vending.Core.Abstractions.Repositories;
using Intravision.Vending.Core.Models;
using Intravision.Vending.DAL.Context;

namespace Intravision.Vending.DAL.Repositories;

public class CoinRepository : Repository<Coin, Guid>, ICoinRepository
{
    public CoinRepository(EfContext context)
        : base(context)
    {

    }
}
