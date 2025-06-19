using AutoMapper;
using Intravision.Vending.Core.Abstractions.Repositories;
using Intravision.Vending.Core.Abstractions.Services;
using Intravision.Vending.Core.DTO.Brand;
using Intravision.Vending.Core.Models;
using Microsoft.Extensions.Logging;

namespace Intravision.Vending.Core.Services;

public class BrandService : IBrandService
{
    private readonly ILogger<BrandService> _logger;

    private readonly IBrandRepository _rep;

    private readonly IMapper _mapper;

    public BrandService(
        ILogger<BrandService> logger,
        IBrandRepository rep,
        IMapper mapper)
    {
        _logger = logger;
        _rep = rep;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BrandGetResponse>> GetAll(CancellationToken token)
    {
        IEnumerable<Brand> brands = await _rep.GetAllAsync(token);
        return _mapper.Map<IEnumerable<BrandGetResponse>>(brands);
    }
}
