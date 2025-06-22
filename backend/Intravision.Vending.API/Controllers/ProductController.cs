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
        [FromQuery] Guid? brandId,
        [FromQuery] int? minPrice,
        [FromQuery] int? maxPrice,
        CancellationToken token = default)
    {
        _logger.LogInformation("Получение списка продуктов. Фильтры: brand={Brand}, minPrice={MinPrice}, maxPrice={MaxPrice}",
            brandId?.ToString() ?? "не указано",
            minPrice?.ToString() ?? "не указано",
            maxPrice?.ToString() ?? "не указано");

        try
        {
            var products = await _product.GetProducts(brandId, minPrice, maxPrice, token);

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

    [HttpPost("import")]
    public async Task<IActionResult> ImportFromExcel(IFormFile file, CancellationToken token)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Файл не предоставлен или пустой.");

        try
        {
            await using var stream = new MemoryStream();
            await file.CopyToAsync(stream, token);
            stream.Position = 0;

            bool result = await _product.ImportFromExcel(stream, token);

            if (result)
                return Ok("Импорт выполнен успешно.");
            else
                return StatusCode(500, "Произошла ошибка при импорте данных.");
        }
        catch (ArgumentException ex)
        {
            return BadRequest($"Ошибка данных: {ex.Message}");
        }
        catch (OperationCanceledException)
        {
            return StatusCode(499, "Операция была отменена.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Внутренняя ошибка сервера: {ex.Message}");
        }
    }
}