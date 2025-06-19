using Intravision.Vending.Core.Abstractions.Services;
using Intravision.Vending.Core.DTO.Order;
using Microsoft.AspNetCore.Mvc;

namespace Intravision.Vending.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(
        IOrderService orderService,
        ILogger<OrdersController> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<OrderCreateResponse> Create([FromBody] OrderCreateRequest request, CancellationToken cancellationToken)
    {
        return await _orderService.CreateOrder(request, cancellationToken);
    }
}