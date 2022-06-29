using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductCategory;

namespace Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository : IRepository<long, ProductCategory>
    {
        EditProductCategoryModel GetDetailBy(long id);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel productCategorySearch);
        List<ProductCategoryViewModel> GetProductCategories();
    }

}
