using System.Text.Json;
using Microsoft.Extensions.Logging;
using Core.Entities;
namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                var basePath = @"G:\Skinet\Infrastructure\Data\SeedData";

          
                // Get base directory of executing assembly
                if (!context.ProdcutBrands.Any())
                {
                  
                     
                    var brandsData = File.ReadAllText(Path.Combine(basePath, "Brands.json"));
                    var brands = JsonSerializer.Deserialize<List<ProdcutBrand>>(brandsData);
                    if (brands is not null)
                        context.ProdcutBrands.AddRange(brands);

                    await context.SaveChangesAsync();
                }
                if (!context.ProductTypes.Any())
                {

                    var typesData = File.ReadAllText(Path.Combine(basePath, "Types.json"));
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    if (types is not null)
                        context.ProductTypes.AddRange(types);

                    await context.SaveChangesAsync();
                }
                if (!context.Products.Any())
                {
                    var ProductsData = File.ReadAllText(Path.Combine(basePath, "Products.json"));
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                    if (Products is not null)
                        context.Products.AddRange(Products);

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}