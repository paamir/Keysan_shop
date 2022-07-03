using System.Collections.Generic;
using Domain.ProductPictureAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductPicture;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductPictures
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string MessageFail { get; set;}
        [TempData]
        public string MessageSuccess { get; set;}
        public ProductPictureSearchModel SearchModel;
        public SelectList Products;
        public List<ProductPictureViewModel> ProductPictures;
        private readonly IProductPictureApplication _productPictureApplication;
        private readonly IProductApplication _productApplication;

        public IndexModel(IProductPictureApplication productPictureApplication, IProductApplication productApplication)
        {
            _productPictureApplication = productPictureApplication;
            _productApplication = productApplication;
        }

        public void OnGet(ProductPictureSearchModel searchModel)
        {
            ProductPictures = _productPictureApplication.Search(searchModel);
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }

        public IActionResult OnGetCreate()
        {
            var product = new ProductPictureCreateModel()
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Create", product);
        }

        public JsonResult OnPostCreate(ProductPictureCreateModel productCategory)
        {
            var result  = _productPictureApplication.Create(productCategory);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var product = _productPictureApplication.GetDetailBy(id);
            product.Products = _productApplication.GetProducts();
            return Partial("./Edit", product);
        }

        public JsonResult OnPostEdit(ProductPictureEditModel command)
        {
            var result = _productPictureApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRestore(long id)
        {
            var result = _productPictureApplication.Restore(id);
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

        public IActionResult OnGetDelete(long id)
        {
            var result = _productPictureApplication.Delete(id);
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
