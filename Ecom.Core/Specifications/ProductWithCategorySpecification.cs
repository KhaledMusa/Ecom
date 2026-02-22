using Ecom.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Specifications
{
    public class ProductWithCategorySpecification : BaseSpecification<Product>
    {
        public ProductWithCategorySpecification(ProductSpecParams productParams) 
            : base(x => 
                (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
                (!productParams.CategoryId.HasValue || x.CategoryId == productParams.CategoryId)
            )
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.Photos);
            AddOrderBy(x => x.Name);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.NewPrice);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.NewPrice);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }

        public ProductWithCategorySpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.Photos);
        }

        private void AddOrderBy(System.Linq.Expressions.Expression<Func<Product, object>> orderByExpression)
        {
             ApplyOrderBy(orderByExpression);
        }
        private void AddOrderByDescending(System.Linq.Expressions.Expression<Func<Product, object>> orderByDescExpression)
        {
             ApplyOrderByDescending(orderByDescExpression);
        }

    }
}
