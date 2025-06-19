using AutoMapper;
using Intravision.Vending.Core.DTO.Product;
using Intravision.Vending.Core.Models;

namespace Intravision.Vending.Core.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductGetResponse>();

        CreateMap<ProductImage, ProductImageGetResponse>();
    }
}
