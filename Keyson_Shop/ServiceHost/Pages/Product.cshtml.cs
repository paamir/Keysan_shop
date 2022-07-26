using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _01_Keyson_Shop_Query.Contract.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IProductQuery _productQuery;
        public ProductQueryModel Product;

        public ProductModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public void OnGet(string id)
        {
           Product = _productQuery.FindProductBy(id);

        }
    }
}
