

using Core.Entities;

namespace Core.Specifications
{
    public class ProductCountWithFilteringSpecification: BaseSpecification<Product>
    {
        public ProductCountWithFilteringSpecification(
          ProductSpecParams prodcutParams

           ) : base(x =>
             (string.IsNullOrEmpty(prodcutParams.Search) || x.Name.ToLower().Contains(prodcutParams.Search)) &&
             (!prodcutParams.BrandId.HasValue || x.ProdcutBrandId == prodcutParams.BrandId) &&
             (!prodcutParams.TypeId.HasValue || x.ProductTypeId == prodcutParams.TypeId)

           )
        {

        }
    }
}
