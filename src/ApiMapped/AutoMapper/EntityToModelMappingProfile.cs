using ApiMapped.Data.Models;
using ApiMapped.Domain.Entities;
using AutoMapper;

namespace ApiMapped.AutoMapper
{
    public class EntityToModelMappingProfile : Profile
    {
        public EntityToModelMappingProfile()
        {
            CreateMap<Product, ProductModel>()
                .ForMember(dest => dest.Ean13, opt => opt.MapFrom(src => src.CodeBar))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

            CreateMap<Category, CategoryModel>();
        }
    }
}
