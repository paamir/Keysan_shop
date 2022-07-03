using System.Collections.Generic;
using System.Linq;
using _01_Keyson_Shop_Query.Contract.Slide;
using ShopManagement.Infrastructure.EFCore;

namespace _01_Keyson_Shop_Query.Implementation
{
    public class SlideQuery : ISlideQuery
    {
        private readonly ShopContext _context;

        public SlideQuery(ShopContext context)
        {
            _context = context;
        }

        public List<SlideQueryModel> GetSlides()
        {
           return _context.Slides
                .Where(x => x.IsDeleted == false)
                .Select(x => new SlideQueryModel
                {
                    ButtonText = x.ButtonText,
                    Header = x.Header,
                    Link = x.Link,
                    Picture = x.Picture,
                    Title = x.Title,
                    Text = x.Text,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle
                }).ToList();
        }
    }
}