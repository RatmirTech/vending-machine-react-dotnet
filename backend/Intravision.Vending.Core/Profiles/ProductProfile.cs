using AutoMapper;
using Intravision.Vending.Core.DTO.Product;
using Intravision.Vending.Core.Models;

namespace Intravision.Vending.Core.Profiles;

/// <summary>
/// Профиль AutoMapper для маппинга сущностей товаров и изображений в DTO.
/// </summary>
public class ProductProfile : Profile
{
    public ProductProfile()
    {
        /// <summary>
        /// Маппинг из <see cref="Product"/> в <see cref="ProductGetResponse"/>.
        /// </summary>
        CreateMap<Product, ProductGetResponse>();

        /// <summary>
        /// Маппинг из <see cref="ProductImage"/> в <see cref="ProductImageGetResponse"/>.
        /// </summary>
        CreateMap<ProductImage, ProductImageGetResponse>();
    }
}
