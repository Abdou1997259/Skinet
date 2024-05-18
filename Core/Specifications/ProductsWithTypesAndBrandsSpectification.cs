using Core.Entities;


namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpectification:BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpectification()
        {
            AddInclude(x=>x.ProdcutBrands);
            AddInclude(x => x.ProductType);
        }
        public ProductsWithTypesAndBrandsSpectification(int id):base(x=>x.Id==id)
        {
            AddInclude(x => x.ProdcutBrands);
            AddInclude(x => x.ProductType);

        }

    }
}
