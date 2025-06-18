using Intravision.Vending.Core.Abstractions;
using Intravision.Vending.Core.Abstractions.Repositories;
using Intravision.Vending.DAL.Context;
using Intravision.Vending.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Intravision.Vending.DAL;

/// <summary>
/// Регистрация зависимостей проекта Intravision.Vending.DAL
/// </summary>
public static class Entry
{
    /// <summary>
    /// Регистрация зависимостей проекта, репозитории
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddRepositories(this IServiceCollection services, string efConnectionString)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddDbContext<EfContext>(o => o.UseNpgsql(connectionString: efConnectionString));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductImageRepository, ProductImageRepository>();
        services.AddScoped<ICoinRepository, CoinRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();

        return services;
    }
}