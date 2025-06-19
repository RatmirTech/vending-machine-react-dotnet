using Intravision.Vending.Core.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace Intravision.Vending.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _product;

    public ProductController(
        ILogger<ProductController> logger,
        IProductService product)
    {
        _logger = logger;
        _product = product;
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetProducts(
        [FromQuery] Guid? brand,
        [FromQuery] int? minPrice,
        [FromQuery] int? maxPrice,
        CancellationToken token = default)
    {
        _logger.LogInformation("Получение списка продуктов. Фильтры: brand={Brand}, minPrice={MinPrice}, maxPrice={MaxPrice}",
            brand?.ToString() ?? "не указано",
            minPrice?.ToString() ?? "не указано",
            maxPrice?.ToString() ?? "не указано");

        try
        {
            var products = await _product.GetProducts(brand, minPrice, maxPrice, token);

            _logger.LogInformation("Список продуктов успешно получен. Кол-во: {Count}", products?.Count() ?? 0);

            return Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении списка продуктов.");
            return StatusCode(500, new { message = "Ошибка сервера при получении продуктов." });
        }
    }

    [HttpGet("price-range")]
    public async Task<IActionResult> GetPriceRange(
        [FromQuery] Guid? brand,
        CancellationToken token = default)
    {
        _logger.LogInformation("Получение диапазона цен. Бренд: {Brand}", brand?.ToString() ?? "не указано");

        try
        {
            var range = await _product.GetPriceRange(brand);

            _logger.LogInformation("Диапазон цен успешно получен: от {Min} до {Max}", range.MinPrice, range.MaxPrice);

            return Ok(range);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении диапазона цен.");
            return StatusCode(500, new { message = "Ошибка сервера при получении диапазона цен." });
        }
    }
}