using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
    public class IndexModel : PageModel
    {
        public ProductCategorySearchModel SearchModel;
        [TempData]
        public string MessageFail { get; set; }
        [TempData]
        public string MessageSuccess { get; set; }
        public List<ProductCategoryViewModel> ProductCategories;
        private readonly IProductCategoryApplication _productCategoryApplication;

        public IndexModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }

        public void OnGet(ProductCategorySearchModel searchModel)
        {
            ProductCategories = _productCategoryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateProductCategoryModel());
        }

        public JsonResult OnPostCreate(CreateProductCategoryModel productCategory)
        {
            var result = _productCategoryApplication.Create(productCategory);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(int id)
        {
            var productCategory = _productCategoryApplication.GetDetail(id);
            return Partial("./Edit", productCategory);
        }

        public JsonResult OnPostEdit(EditProductCategoryModel command)
        {
            var result = _productCategoryApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetDelete(long id)
        {
            var result = _productCategoryApplication.unVisible(id);
            if (result.IsSuccedded)
            {
                MessageSuccess = result.Message;
            }
            else
            {
                MessageFail = result.Message;
            }

            return RedirectToPage("./Index");
        }
        public IActionResult OnGetRestore(long id)
        {
            var result = _productCategoryApplication.Visible(id);
            if (result.IsSuccedded)
            {
                MessageSuccess = result.Message;
            }
            else
            {
                MessageFail = result.Message;
            }

            return RedirectToPage("./Index");
        }

    }
}