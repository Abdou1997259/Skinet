using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetAllProductsAsync();
        Task<IReadOnlyList<ProdcutBrand>> GetAllProdcutBrandsAsync();
        Task<IReadOnlyList<ProductType>> GetAllProdcutTypesAsync();

    }
}