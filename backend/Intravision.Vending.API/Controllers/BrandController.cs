using Intravision.Vending.Core.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace Intravision.Vending.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrandController : ControllerBase
{
    private readonly ILogger<BrandController> _logger;
    private readonly IBrandService _brand;

    public BrandController(
        ILogger<BrandController> logger,
        IBrandService brand)
    {
        _logger = logger;
        _brand = brand;
    }

    [HttpGet]
    public async Task<IActionResult> GetBrand(CancellationToken token = default)
    {
        _logger.LogInformation("Получение списка брендов начато.");

        try
        {
            var result = await _brand.GetAll(token);

            _logger.LogInformation("Список брендов получен успешно. Кол-во: {Count}", result?.Count() ?? 0);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении списка брендов");
            return StatusCode(500, new { message = "Ошибка сервера при получении брендов" });
        }
    }
}
