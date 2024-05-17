using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data.Config;

namespace Skinet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IProductRepository repo) : ControllerBase
    {

      
        [HttpGet]
        
        public async Task<ActionResult<List<Product>>> GetProducts(){
            var  products=await repo.GetAllProductsAsync();
            return Ok(products);
        }
        [HttpGet]
        [Route("{Id}")]
        public async Task<ActionResult<Product>> GetById(int Id){
            var product=await repo.GetProductByIdAsync(Id);
            return product;
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProdcutBrand>>> GetProdcutBrands()
        {
            return Ok(await repo.GetAllProdcutBrandsAsync()) ;
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypesBrands()
        {
            return Ok(await repo.GetAllProdcutTypesAsync());
        }

    }
}