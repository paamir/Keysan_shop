using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.Products
{
    public class IndexModel : PageModel
    {
        [TempData] public string MessageFail { get; set; }
        [TempData] public string MessageSuccess { get; set; }
        public ProductSearchModel SearchModel;
        public SelectList ProductCategories;
        public List<ProductViewModel> Products;
        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;

        public IndexModel(IProductApplication productApplication,
            IProductCategoryApplication productCategoryApplication)
        {
            _productApplication = productApplication;
            _productCategoryApplication = productCategoryApplication;
        }

        public void OnGet(ProductSearchModel searchModel)
        {
            Products = _productApplication.Search(searchModel);
            ProductCategories = new SelectList(_productCategoryApplication.GetProductCategories(), "Id", "Name");
        }

        public IActionResult OnGetCreate()
        {
            var product = new ProductCreateModel
            {
                Categories = _productCategoryApplication.GetProductCategories()
            };
            return Partial("./Create", product);
        }

        public JsonResult OnPostCreate(ProductCreateModel productCategory)
        {
            var result = _productApplication.Create(productCategory);
            if (result.IsSuccedded)
            {
                MessageSuccess = result.Message;
            }
            else
            {
                MessageFail = result.Message;
            }
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(int id)
        {
            var product = _productApplication.GetDetailBy(id);
            product.Categories = _productCategoryApplication.GetProductCategories();
            return Partial("./Edit", product);
        }

        public JsonResult OnPostEdit(ProductEditModel command)
        {
            var result = _productApplication.Edit(command);
            if (result.IsSuccedded)
            {
                MessageSuccess = result.Message;
            }
            else
            {
                MessageFail = result.Message;
            }
            return new JsonResult(result);
        }
    }
}
