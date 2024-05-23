using Core.Entities;


namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpectification:BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpectification(SortOptions sort)
        {
            AddInclude(x=>x.ProdcutBrands);
            AddInclude(x => x.ProductType);
              
            if(sort !=0)
            {
                switch(sort)
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
