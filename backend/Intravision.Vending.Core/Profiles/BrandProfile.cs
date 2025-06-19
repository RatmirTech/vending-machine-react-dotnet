using AutoMapper;
using Intravision.Vending.Core.DTO.Brand;
using Intravision.Vending.Core.Models;

namespace Intravision.Vending.Core.Profiles;

public class BrandProfile : Profile
{
    public BrandProfile()
    {
        CreateMap<Brand, BrandGetResponse>();
    }
}
