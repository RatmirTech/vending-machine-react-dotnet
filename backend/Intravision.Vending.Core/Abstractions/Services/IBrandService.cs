using Intravision.Vending.Core.DTO.Brand;

namespace Intravision.Vending.Core.Abstractions.Services;

public interface IBrandService
{
    Task<IEnumerable<BrandGetResponse>> GetAll(CancellationToken cancellationToken = default);
}
