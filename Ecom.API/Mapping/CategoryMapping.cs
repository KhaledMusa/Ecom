using AutoMapper;
using Ecom.Core.DTO_s;
using Ecom.Core.Entities.Product;

namespace Ecom.API.Mapping
{
    public class CategoryMapping:Profile
    {
        public CategoryMapping()
        {
            CreateMap<CategoryDTO,Category>().ReverseMap();
            CreateMap<UpdatecategoryDTO,Category>().ReverseMap();
        }
    }
}
