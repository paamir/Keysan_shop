using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _01_Keyson_Shop_Query.Contract.Product;
using _01_Keyson_Shop_Query.Contract.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class SearchModel : PageModel
    {
        public List<ProductQueryModel> Products;
        private readonly IProductQuery _productQuery;
        public string Value { get; set; }

        public SearchModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }


        public void OnGet(string value)
        {
            Value = value;
            Products = _productQuery.Search(Value);
        }

    }
}
