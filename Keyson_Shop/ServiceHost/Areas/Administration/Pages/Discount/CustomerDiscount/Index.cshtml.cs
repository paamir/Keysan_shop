using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscountManagement.Application.Contract.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Discount.CustomerDiscount
{
    public class IndexModel : PageModel
    {
        [TempData] public string MessageFail { get; set; }
        [TempData] public string MessageSuccess { get; set; }
        public CustomerDiscountSearchModel SearchModel;
        private readonly ICustomerDiscountApplication _customerDiscountApplication;
        private readonly IProductApplication _productApplication;
        public List<CustomerDiscountViewModel> Discounts;
        public SelectList Products;
        public IndexModel(ICustomerDiscountApplication customerDiscountApplication, IProductApplication productApplication)
        {
            _customerDiscountApplication = customerDiscountApplication;
            _productApplication = productApplication;
        }

        public void OnGet(CustomerDiscountSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            Discounts = _customerDiscountApplication.Search(searchModel);
        }
        public IActionResult OnGetCreate()
        {
            var product = new CustomerDiscountCreateModel()
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Create", product);
        }

        public JsonResult OnPostCreate(CustomerDiscountCreateModel command)
        {
            var result = _customerDiscountApplication.Create(command);
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
            var discount = _customerDiscountApplication.GetDetailBy(id);
            discount.Products = _productApplication.GetProducts();
            return Partial("./Edit", discount);
        }

        public JsonResult OnPostEdit(CustomerDiscountEditModel command)
        {
            var result = _customerDiscountApplication.Edit(command);
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
