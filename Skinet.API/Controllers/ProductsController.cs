using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data.Config;
using Core.Specifications;
using Skinet.API.DTOs;
using AutoMapper;

namespace Skinet.API.Controllers
{

    public class ProductsController(IGenericRepository<Product> prodcutRepo,
        IGenericRepository<ProductType> productTypeRepo,
        IGenericRepository<ProdcutBrand> productBrands,
        IMapper _mapper
        
        )
        
        : BaseApiController
    {

      
        [HttpGet]      
        public async Task<ActionResult<IReadOnlyList<ProdcutDto>>> GetProducts(){
            var spec = new ProductsWithTypesAndBrandsSpectification();
            var  products=await prodcutRepo.ListAync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProdcutDto>>(products));
        }
        [HttpGet]
        [Route("{Id}")]
        public async Task<ActionResult<ProdcutDto>> GetById(int Id){
            var spec = new ProductsWithTypesAndBrandsSpectification(Id);
            var product=await prodcutRepo.GetEntityWithSpec(spec);
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