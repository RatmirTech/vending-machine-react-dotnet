using Intravision.Vending.Core.Abstractions;
using Intravision.Vending.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Intravision.Vending.Core;

/// <summary>
/// Регистрация зависимостей проекта Intravision.Vending.Core
/// </summary>
public static class Entry
{
    /// <summary>
    /// Регистрация зависимостей проекта
    /// </summary>
    /// <param name="services">сервисы</param>
    /// <returns>Контейнер зависимостей</returns>
    static public IServiceCollection AddCore(this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddSingleton<IVendingSessionService, VendingSessionService>();

        return services;
    }
}