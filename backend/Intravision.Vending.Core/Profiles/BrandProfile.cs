using AutoMapper;
using Intravision.Vending.Core.DTO.Brand;
using Intravision.Vending.Core.Models;

namespace Intravision.Vending.Core.Profiles;

/// <summary>
/// Профиль AutoMapper для сопоставления сущности <see cref="Brand"/> с DTO <see cref="BrandGetResponse"/>.
/// </summary>
public class BrandProfile : Profile
{
    public BrandProfile()
    {
        /// <summary>
        /// Маппинг из <see cref="Brand"/> в <see cref="BrandGetResponse"/>.
        /// </summary>
        CreateMap<Brand, BrandGetResponse>();
    }
}