using Intravision.Vending.Core.Abstractions.Repositories;
using Intravision.Vending.Core.Models;
using Intravision.Vending.DAL.Context;

namespace Intravision.Vending.DAL.Repositories;

public class OrderItemRepository : Repository<OrderItem, Guid>, IOrderItemRepository
{
    public OrderItemRepository(EfContext context)
        : base(context)
    {

    }
}
