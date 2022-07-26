using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _01_Keyson_Shop_Query.Contract.Product;
using _01_Keyson_Shop_Query.Contract.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class LatestArrivalsViewComponent : ViewComponent
    {
        private readonly IProductQuery _productQuery;
        public LatestArrivalsViewComponent(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public IViewComponentResult Invoke()
        {
            var LatestProduct = _productQuery.LatestArrivals();
            return View(LatestProduct);
        }
    }
}
