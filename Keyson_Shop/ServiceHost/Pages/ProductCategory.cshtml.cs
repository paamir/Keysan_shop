using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _01_Keyson_Shop_Query.Contract.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductCategoryModel : PageModel
    {
        public ProductCategoryQueryModel productCategory;
        private readonly IProductCategoryQuery _productCategoryQuery;

        public ProductCategoryModel(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }

        public void OnGet(string id)
        {
            productCategory = _productCategoryQuery.GetProductCategoriesWithProducts(id);
        }

    }
}
