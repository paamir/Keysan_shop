using System.Collections.Generic;
using System.Reflection.Emit;
using _0_Framework.Application;
using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository : IRepository<long, ProductCategory>
    {
        EditProductCategoryModel GetDetailBy(long id);

        List<ProductCategoryViewModel> Search(ProductCategorySearchModel productCategorySearch);
        List<ProductCategoryViewModel> GetProductCategories();
    }

}
