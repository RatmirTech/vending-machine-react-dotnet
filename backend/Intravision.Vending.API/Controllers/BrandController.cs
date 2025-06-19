using Intravision.Vending.Core.Abstractions.Services;
using Intravision.Vending.Core.DTO.Brand;
using Microsoft.AspNetCore.Mvc;

namespace Intravision.Vending.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrandController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;

    private readonly IBrandService _brand;

    public BrandController(
        ILogger<ProductController> logger,
        IBrandService brand)
    {
        _logger = logger;
        _brand = brand;
    }

    [HttpGet]
    public async Task<IEnumerable<BrandGetResponse>> GetBrand(
        CancellationToken token = default)
    {
        return await _brand.GetAll(token);
    }
}