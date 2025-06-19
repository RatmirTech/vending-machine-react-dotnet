using Intravision.Vending.Core.Models;

namespace Intravision.Vending.Core.Abstractions.Repositories;

public interface IOrderItemRepository : IRepository<OrderItem, Guid>
{
}
