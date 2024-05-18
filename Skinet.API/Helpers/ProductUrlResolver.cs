using AutoMapper;
using AutoMapper.Execution;
using Core.Entities;
using Skinet.API.DTOs;
using congif= Microsoft.Extensions.Configuration;
namespace Skinet.API.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProdcutDto, string>
    {
        private readonly congif.IConfiguration _configuration;
        public ProductUrlResolver(congif.IConfiguration config )
        {
            _configuration = config;
        }

        public string Resolve(Product source, ProdcutDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrWhiteSpace(source.PictureUrl))
            {
                return _configuration["ApiUrl"] + source.PictureUrl;
            }
            return null;
        }
    }
}
