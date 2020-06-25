using System.Linq;
using ApiMapped.Domain.Entities;
using ApiMapped.ViewModels;
using AutoMapper;

namespace ApiMapped.AutoMapper
{
    public class EntityToViewModelMappingProfile : Profile
    {
        public EntityToViewModelMappingProfile()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.CategoryName))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Prices.Last().Price));
        }
    }
}
