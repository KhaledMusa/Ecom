using Ecom.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Interfaces
{
    public interface ICategoryRepository:IGenericRepositry<Category>
    {
        //Task<IReadOnlyList<Category>> GetCategoriesWithProductsAsync();
        //Task<Category> GetCategoryWithProductsAsync(int id);
        //Task<IReadOnlyList<Category>> GetCategoriesWithProductsAsync(params string[] includes);
        //Task<Category> GetCategoryWithProductsAsync(int id, params string[] includes);
    }
}
