using System.Collections.Generic;
using Domain.ProductPictureAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Application.Contracts.Slide;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slides
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string MessageFail { get; set;}
        [TempData]
        public string MessageSuccess { get; set;}


        public List<SlideViewModel> Slides;
        private readonly ISlideApplication _slideApplication;

        public IndexModel(ISlideApplication slideApplication)
        {
            _slideApplication = slideApplication;
        }

        public void OnGet(ProductPictureSearchModel searchModel)
        {
            Slides = _slideApplication.GetAll();
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new SlideCreateModel());
        }

        public JsonResult OnPostCreate(SlideCreateModel slide)
        {
            var result  = _slideApplication.Create(slide);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var slide = _slideApplication.GetDetail(id);
            return Partial("./Edit", slide);
        }

        public JsonResult OnPostEdit(SlideEditModel command)
        {
            var result = _slideApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRestore(long id)
        {
            var result = _slideApplication.Restore(id);
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
            var result = _slideApplication.Delete(id);
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
