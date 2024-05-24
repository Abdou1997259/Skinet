using Core.Entities;


namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpectification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpectification(
           ProductSpecParams prodcutParams

            ) : base(x =>
              (string.IsNullOrEmpty(prodcutParams.Search) || x.Name.ToLower().Contains(prodcutParams.Search)) &&
              (!prodcutParams.BrandId.HasValue || x.ProdcutBrandId== prodcutParams.BrandId) &&
              (!prodcutParams.TypeId.HasValue || x.ProductTypeId== prodcutParams.TypeId)
            
            )
        {
            AddInclude(x=>x.ProdcutBrands);
            AddInclude(x => x.ProductType);

            ApplyingPaging(prodcutParams.PageSize * (prodcutParams.PageIndex - 1),
                prodcutParams.PageSize);

            if(prodcutParams.SortOptions != 0)
            {
                switch(prodcutParams.SortOptions)
                {
                    case SortOptions.PRICE:
                        AddOrderBy(x => x.Price);
                        break;
                    case SortOptions.PRICEASC:
                        AddOrderByDecending(x => x.Price);
                        break;

                    default:
                        AddOrderBy(x => x.Name);
                        break;

                }
            }
        }
        public ProductsWithTypesAndBrandsSpectification(int id):base(x=>x.Id==id)
        {
            AddInclude(x => x.ProdcutBrands);
            AddInclude(x => x.ProductType);

        }

    }
}
