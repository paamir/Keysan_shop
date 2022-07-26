using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Application.Contract.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Discount.ColleagueDiscount
{
    public class IndexModel : PageModel
    {
        [TempData] public string MessageFail { get; set; }
        [TempData] public string MessageSuccess { get; set; }
        public ColleagueDiscountSearchModel SearchModel;
        private readonly IColleagueDiscountApplication _colleagueDiscountApplication;
        private readonly IProductApplication _productApplication;
        public List<ColleagueDiscountViewModel> Discounts;
        public SelectList Products;
        public IndexModel(IColleagueDiscountApplication colleagueDiscountApplication, IProductApplication productApplication)
        {
            _colleagueDiscountApplication = colleagueDiscountApplication;
            _productApplication = productApplication;
        }

        public void OnGet(ColleagueDiscountSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            Discounts = _colleagueDiscountApplication.Search(searchModel);
        }
        public IActionResult OnGetCreate()
        {
            var product = new ColleagueDiscountCreateModel()
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Create", product);
        }

        public JsonResult OnPostCreate(ColleagueDiscountCreateModel command)
        {
            var result = _colleagueDiscountApplication.Create(command);
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
            var discount = _colleagueDiscountApplication.GetDetailBy(id);
            discount.Products = _productApplication.GetProducts();
            return Partial("./Edit", discount);
        }

        public JsonResult OnPostEdit(ColleagueDiscountEditModel command)
        {
            var result = _colleagueDiscountApplication.Edit(command);
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
