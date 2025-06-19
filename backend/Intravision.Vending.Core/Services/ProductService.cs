using AutoMapper;
using Intravision.Vending.Core.Abstractions.Repositories;
using Intravision.Vending.Core.Abstractions.Services;
using Intravision.Vending.Core.DTO.Product;
using Intravision.Vending.Core.Models;
using Microsoft.Extensions.Logging;

namespace Intravision.Vending.Core.Services;

public class ProductService : IProductService
{
    private readonly ILogger<ProductService> _logger;

    private readonly IProductRepository _products;

    private readonly IProductImageRepository _images;

    private readonly IMapper _mapper;

    public ProductService(
        ILogger<ProductService> logger,
        IProductRepository products,
        IProductImageRepository images,
        IMapper mapper)
    {
        _logger = logger;
        _products = products;
        _images = images;
        _mapper = mapper;
    }

    public async Task<PriceRangeDto> GetPriceRange(Guid? brandId, CancellationToken token = default)
    {
        return await _products.GetPriceRangeAsync(brandId, token);
    }

    public async Task<IEnumerable<ProductGetResponse>> GetProducts(
        Guid? brandId,
        int? minPrice,
        int? maxPrice,
        CancellationToken token = default)
    {
        IEnumerable<Product> products = await _products.GetFilteredAsync(brandId, minPrice, maxPrice, token);
        IEnumerable<ProductGetResponse> responses = _mapper.Map<IEnumerable<ProductGetResponse>>(products);
        foreach (ProductGetResponse response in responses)
        {
            IEnumerable<ProductImage> images = await _images.GetByProductIdAsync(response.Id, token);
            response.Images = _mapper.Map<IEnumerable<ProductImageGetResponse>>(images);
        }
        return responses;
    }
}