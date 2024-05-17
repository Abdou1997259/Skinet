using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _db;

        public ProductRepository(StoreContext db)
        {
            _db = db;
        }

        public async Task<IReadOnlyList<ProdcutBrand>> GetAllProdcutBrandsAsync()
        {
            return await _db.ProdcutBrands.ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetAllProdcutTypesAsync()
        { 
            return await _db.ProductTypes.ToListAsync();
        }

        public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
        {
           return await _db.Products
                    .Include(p=>p.ProdcutBrands)
                    .Include(p=>p.ProductType)
                    .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _db.Products
                       .Include(p => p.ProdcutBrands)
                       .Include(p => p.ProductType)
                       .FirstOrDefaultAsync(p=> p.Id == id);


                   
        }
    }
}