using AutoMapper;
using Ecom.Core.DTO_s;
using Ecom.Core.Entities.Product;

namespace Ecom.API.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product,ProductDTO >().ForMember(x=>x.CategoryName,op=>op.MapFrom(src=>src.Category.Name)).ReverseMap();
            CreateMap<Photo, PhotoDTO>().ReverseMap();
            CreateMap<AddProductDTO, Product>()
                .ForMember(dest => dest.Photos, opt => opt.Ignore()) 
                .ReverseMap();
            CreateMap<UpdateProductDTO, Product>()
               .ForMember(dest => dest.Photos, opt => opt.Ignore())
               .ReverseMap();
        }
    }
}
