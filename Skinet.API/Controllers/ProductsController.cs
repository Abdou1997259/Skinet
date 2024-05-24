using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data.Config;
using Core.Specifications;
using Skinet.API.DTOs;
using AutoMapper;
using Skinet.API.Helpers;
using Skinet.API.Errors;
using Microsoft.AspNetCore.Http;

namespace Skinet.API.Controllers
{

    public class ProductsController(IGenericRepository<Product> prodcutRepo,
        IGenericRepository<ProductType> productTypeRepo,
        IGenericRepository<ProdcutBrand> productBrands,
        IMapper _mapper
        
        ): BaseApiController
    {

      
        [HttpGet]      
        public async Task<ActionResult<Pagination<IReadOnlyList<ProdcutDto>>>> GetProducts(
            [FromQuery] ProductSpecParams productParams ){

            var spec = new ProductsWithTypesAndBrandsSpectification(productParams);

            var specCount=new ProductCountWithFilteringSpecification(productParams);
            var count=await prodcutRepo.CountAsync(specCount);
            var  products=await prodcutRepo.ListAync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProdcutDto>>(products);
            return Ok(new Pagination<ProdcutDto>
            {
                Count=count,
                Data= data,
                PageSize=productParams.PageSize,
                PageIndex=productParams.PageIndex

            });
        }
        [HttpGet]
        [Route("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProdcutDto>> GetById(int Id){
            var spec = new ProductsWithTypesAndBrandsSpectification(Id);
            
            var product=await prodcutRepo.GetEntityWithSpec(spec);

            if (product is null)
                return NotFound(new ApiResponse(404));

            return _mapper.Map<Product,ProdcutDto>(product);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProdcutBrand>>> GetProdcutBrands()
        {
            return Ok(await productBrands.GetAllAsync()) ;
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypesBrands()
        {
            return Ok(await productTypeRepo.GetAllAsync());
        }

    }
}