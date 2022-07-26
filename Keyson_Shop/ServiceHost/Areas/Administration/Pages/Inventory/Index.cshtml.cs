using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using InventoryManagement.Application.Contract.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        [TempData] public string MessageFail { get; set; }
        [TempData] public string MessageSuccess { get; set; }
        public InventorySearchModel SearchModel;
        private readonly IInventoryApplication _inventoryApplication;
        private readonly IProductApplication _productApplication;
        public List<InventoryViewModel> Inventory;
        public SelectList Products;
        public IndexModel(IInventoryApplication inventoryApplication,IProductApplication productApplication)
        {
            _inventoryApplication = inventoryApplication;
            _productApplication = productApplication;
        }

        public void OnGet(InventorySearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            Inventory = _inventoryApplication.Search(searchModel);
        }
        public IActionResult OnGetCreate()
        {
            var product = new InventoryCreateModel()
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Create", product);
        }

        public JsonResult OnPostCreate(InventoryCreateModel command)
        {
            var result = _inventoryApplication.Create(command);
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

        public IActionResult OnGetReduction(int id)
         {
            var Inventory = new InventoryReductionModel()
            {
                InventoryId = id
            };
            return Partial("./Reduction", Inventory);
        }

        public JsonResult OnPostReduction(InventoryReductionModel command)
        {
            var result = _inventoryApplication.Reduction(command);
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
        public IActionResult OnGetIncrease(int id)
        {
            var Inventory = new InventoryIncreaseModel()
            {
                InventoryId = id
            };
            return Partial("./Increase", Inventory);
        }

        public JsonResult OnPostIncrease(InventoryIncreaseModel command)
        {
            var result = _inventoryApplication.Increase(command);
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
        public IActionResult OnGetEdit(long id)
        {
            var Inventory = _inventoryApplication.GetDetailBy(id);
            Inventory.Products = _productApplication.GetProducts();
            return Partial("./Edit", Inventory);
        }

        public JsonResult OnPostEdit(InventoryEditModel command)
        {
            var result = _inventoryApplication.Edit(command);
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

        public IActionResult OnGetLog(long id)
        {
            var InventoryOperations = _inventoryApplication.GetInventoryOperationLog(id);
            return Partial("./InventoryOperationLogs", InventoryOperations);
        }
    }
}
