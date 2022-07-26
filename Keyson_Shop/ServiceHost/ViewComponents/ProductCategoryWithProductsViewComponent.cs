using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _01_Keyson_Shop_Query.Contract.ProductCategory;
using _01_Keyson_Shop_Query.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoryWithProductsViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _productCategoryQuery;

        public ProductCategoryWithProductsViewComponent(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var a = _productCategoryQuery.GetProductCategoriesWithProducts();
            return View(_productCategoryQuery.GetProductCategoriesWithProducts());
        }
    }
}
