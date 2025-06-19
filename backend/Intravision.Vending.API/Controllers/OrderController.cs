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
    public async Task<IActionResult> Create([FromBody] OrderCreateRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Создание нового заказа: {@Request}", request);

        try
        {
            var response = await _orderService.CreateOrder(request, cancellationToken);

            _logger.LogInformation("Заказ создан успешно: {@Response}", response);

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Произошла ошибка при создании заказа");
            return StatusCode(500, new { message = "Внутренняя ошибка сервера" });
        }
    }

}