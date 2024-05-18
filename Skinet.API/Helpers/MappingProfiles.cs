using AutoMapper;
using Core.Entities;
using Skinet.API.DTOs;

namespace Skinet.API.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProdcutDto>()
                 .ForMember(d=>d.ProdcutBrands,o=>o.MapFrom(s=>s.ProdcutBrands.Name))
                 .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                 .ForMember(d=>d.PictureUrl,o=>o.MapFrom<ProductUrlResolver>());
        }
    }
}
