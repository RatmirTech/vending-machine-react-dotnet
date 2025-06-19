using Intravision.Vending.Core.DTO.Order;

namespace Intravision.Vending.Core.Abstractions.Services;

public interface IOrderService
{
    Task<OrderCreateResponse> CreateOrder(OrderCreateRequest request, CancellationToken token = default);
}
