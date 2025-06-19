using Intravision.Vending.Core.Abstractions.Services;
using Intravision.Vending.Core.DTO.Product;
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
    public async Task<IEnumerable<ProductGetResponse>> GetProducts(
        [FromQuery] Guid? brand,
        [FromQuery] int? minPrice,
        [FromQuery] int? maxPrice,
        CancellationToken token = default)
    {
        return await _product.GetProducts(brand, minPrice, maxPrice, token);
    }

    [HttpGet("price-range")]
    public async Task<PriceRangeDto> GetPriceRange(
        [FromQuery] Guid? brand,
        CancellationToken token = default)
    {
        return await _product.GetPriceRange(brand);
    }
}
