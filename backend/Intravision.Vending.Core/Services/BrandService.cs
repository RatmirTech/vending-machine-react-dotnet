using AutoMapper;
using Intravision.Vending.Core.Abstractions.Repositories;
using Intravision.Vending.Core.Abstractions.Services;
using Intravision.Vending.Core.DTO.Brand;
using Intravision.Vending.Core.Models;
using Microsoft.Extensions.Logging;

namespace Intravision.Vending.Core.Services;

/// <summary>
/// Сервис для получения информации о брендах.
/// Реализует бизнес-логику, связанную с доступом к данным брендов через репозиторий.
/// </summary>
public class BrandService : IBrandService
{
    /// <summary>
    /// Логгер для записи служебной информации и ошибок.
    /// </summary>
    private readonly ILogger<BrandService> _logger;

    /// <summary>
    /// Репозиторий брендов.
    /// </summary>
    private readonly IBrandRepository _rep;

    /// <summary>
    /// Маппер для преобразования сущностей в DTO.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Создаёт экземпляр сервиса брендов.
    /// </summary>
    /// <param name="logger">Логгер для логирования действий сервиса.</param>
    /// <param name="rep">Репозиторий для доступа к данным брендов.</param>
    /// <param name="mapper">AutoMapper для преобразования моделей.</param>
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
        return _mapper.Map<IEnumerable<BrandGetResponse>>(brands.OrderBy(x => x.Name).ToList());
    }
}
