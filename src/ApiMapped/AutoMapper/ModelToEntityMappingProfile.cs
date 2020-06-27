using System.Linq;
using ApiMapped.Data.Models;
using ApiMapped.Domain.Entities;
using AutoMapper;

namespace ApiMapped.AutoMapper
{
    public class ModelToEntityMappingProfile : Profile
    {
        private bool withPrice = true;

        public ModelToEntityMappingProfile()
        {

            if (withPrice)
            {
                CreateMap<ProductModel, Product>()
                    .ForMember(dest => dest.CodeBar, opt => opt.MapFrom(src => src.Ean13))
                    .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                    .ForMember(dest => dest.UpperPrice, opt => opt.MapFrom(src =>
                        src.Prices.Where(x => x.Price.Equals(src.Prices.Max(y => y.Price))).FirstOrDefault()))
                    .ForMember(dest => dest.LowerPrice, opt => opt.MapFrom(src =>
                        src.Prices.Where(x => x.Price.Equals(src.Prices.Min(y => y.Price))).FirstOrDefault()));
            }
            else
            {
                CreateMap<ProductModel, Product>()
                    .ForMember(dest => dest.CodeBar, opt => opt.MapFrom(src => src.Ean13))
                    .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
            }

            CreateMap<CategoryModel, Category>();
        }
    }
}