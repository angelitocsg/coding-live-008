using ApiMapped.Data.Models;
using ApiMapped.Domain.Entities;
using AutoMapper;

namespace ApiMapped.AutoMapper
{
    public class ModelToEntityMappingProfile : Profile
    {
        public ModelToEntityMappingProfile()
        {
            CreateMap<ProductModel, Product>()
                .ForMember(dest => dest.CodeBar, opt => opt.MapFrom(src => src.Ean13));
        }
    }
}
