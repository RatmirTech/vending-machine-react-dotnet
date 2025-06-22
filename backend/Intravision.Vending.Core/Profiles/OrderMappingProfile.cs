using AutoMapper;
using Intravision.Vending.Core.DTO.Order;
using Intravision.Vending.Core.Models;

namespace Intravision.Vending.Core.Profiles;

/// <summary>
/// Профиль AutoMapper для маппинга заказов и их позиций из DTO-запросов в сущности.
/// </summary>
public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        /// <summary>
        /// Маппинг из запроса <see cref="OrderCreateRequest"/> в сущность <see cref="Order"/>.
        /// Генерирует новый ID и метку времени при создании заказа.
        /// Игнорирует свойство <c>TotalPrice</c>, так как оно вычисляется отдельно.
        /// </summary>
        CreateMap<OrderCreateRequest, Order>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
            .ForMember(dest => dest.TotalPrice, opt => opt.Ignore());

        /// <summary>
        /// Маппинг из DTO позиции заказа <see cref="OrderItemDto"/> в сущность <see cref="OrderItem"/>.
        /// Некоторые поля (название, цена и т.п.) задаются отдельно на этапе обработки.
        /// </summary>
        CreateMap<OrderItemDto, OrderItem>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.ProductName, opt => opt.Ignore())
            .ForMember(dest => dest.BrandName, opt => opt.Ignore())
            .ForMember(dest => dest.UnitPrice, opt => opt.Ignore())
            .ForMember(dest => dest.Order, opt => opt.Ignore())
            .ForMember(dest => dest.TotalPrice, opt => opt.Ignore());
    }
}
