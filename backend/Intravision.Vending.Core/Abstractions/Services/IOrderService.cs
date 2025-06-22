using Intravision.Vending.Core.DTO.Order;

namespace Intravision.Vending.Core.Abstractions.Services;


/// <summary>
/// Сервис для обработки заказов.
/// </summary>
public interface IOrderService
{
    /// <summary>
    /// Создаёт новый заказ на основе переданных данных.
    /// </summary>
    /// <param name="request">Объект запроса на создание заказа.</param>
    /// <param name="token">Токен отмены для прерывания асинхронной операции.</param>
    /// <returns>
    /// Задача, представляющая асинхронную операцию. 
    /// Результатом является объект <see cref="OrderCreateResponse"/> с данными о созданном заказе.
    /// </returns>
    Task<OrderCreateResponse> CreateOrder(OrderCreateRequest request, CancellationToken token = default);
}
