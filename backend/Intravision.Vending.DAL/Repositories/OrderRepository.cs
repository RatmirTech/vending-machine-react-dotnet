using Intravision.Vending.Core.Abstractions.Repositories;
using Intravision.Vending.Core.Models;
using Intravision.Vending.DAL.Context;

namespace Intravision.Vending.DAL.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(EfContext context)
        : base(context)
    {

    }
}
