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
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Prices.Last().Price))
                .ForMember(dest => dest.UpperPrice, opt => opt.MapFrom(src => src.UpperPrice.Price))
                .ForMember(dest => dest.LowerPrice, opt => opt.MapFrom(src => src.LowerPrice.Price));
        }
    }
}
