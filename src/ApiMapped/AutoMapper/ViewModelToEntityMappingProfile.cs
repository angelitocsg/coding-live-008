using ApiMapped.Domain.Entities;
using ApiMapped.ViewModels;
using AutoMapper;

namespace ApiMapped.AutoMapper
{
    public class ViewModelToEntityMappingProfile : Profile
    {
        public ViewModelToEntityMappingProfile()
        {
            CreateMap<ProductViewModel, Product>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => new Category(src.Category)))
                .ForMember(dest => dest.Prices, opt => opt.MapFrom(src => new PriceHistory[] { new PriceHistory(src.Price) }))
                .ForMember(dest => dest.UpperPrice, opt => opt.MapFrom(src => new PriceHistory[] { new PriceHistory(src.Price) }))
                .ForMember(dest => dest.LowerPrice, opt => opt.MapFrom(src => new PriceHistory[] { new PriceHistory(src.Price) }));
        }
    }
}
