using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _01_Keyson_Shop_Query.Contract;
using _01_Keyson_Shop_Query.Contract.Slide;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly ISlideQuery slideQuery;

        public SliderViewComponent(ISlideQuery slideQuery)
        {
            this.slideQuery = slideQuery;
        }

        public IViewComponentResult Invoke()
        {
            var slides = slideQuery.GetSlides();
            return View(slides);
        }
    }
}
