using Intravision.Vending.Core.DTO.Product;

namespace Intravision.Vending.Core.Abstractions.Services;

public interface IProductService
{
    Task<IEnumerable<ProductGetResponse>> GetProducts(
        Guid? brandId,
        int? minPrice,
        int? maxPrice,
        CancellationToken token = default);

    Task<PriceRangeDto> GetPriceRange(
        Guid? brandId,
        CancellationToken token = default);

    //Task ImportFromExcel();
}
