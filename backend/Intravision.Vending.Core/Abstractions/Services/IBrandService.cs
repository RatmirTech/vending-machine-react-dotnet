using Intravision.Vending.Core.DTO.Brand;

namespace Intravision.Vending.Core.Abstractions.Services;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Сервис для работы с брендами.
/// </summary>
public interface IBrandService
{
    /// <summary>
    /// Асинхронно получает список всех брендов.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены для прерывания асинхронной операции.</param>
    /// <returns>
    /// Задача, представляющая асинхронную операцию. 
    /// Результатом является коллекция объектов <see cref="BrandGetResponse"/>, содержащих данные о брендах.
    /// </returns>
    Task<IEnumerable<BrandGetResponse>> GetAll(CancellationToken cancellationToken = default);
}
