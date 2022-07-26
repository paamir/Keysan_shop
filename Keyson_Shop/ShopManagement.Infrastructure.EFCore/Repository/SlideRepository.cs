using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class SlideRepository : RepositoryBase<long ,Slide>, ISlideRepository
    {
        private readonly ShopContext _context;

        public SlideRepository(ShopContext context): base(context)
        {
            _context = context;
        }

        public List<SlideViewModel> GetAll()
        {
            return _context.Slides.Select(x => new SlideViewModel()
            {
                ButtonText = x.ButtonText,
                CreationDate = x.CreationDate.ToFarsi(),
                Header = x.Header,
                Id = x.Id,
                IsDeleted = x.IsDeleted,
                Picture = x.Picture,
                Text = x.Text,
                Title = x.Title,
                Link = x.Link
            }).AsNoTracking().ToList();
        }

        public SlideEditModel GetDetail(long id)
        {
           return _context.Slides.Select(x => new SlideEditModel()
           {
               ButtonText = x.ButtonText,
               Header = x.Header,
               Id = x.Id,
               IsDeleted = x.IsDeleted,
               Picture = x.Picture,
               Text = x.Text,
               Title = x.Title,
               PictureAlt = x.PictureAlt,
               PictureTitle = x.PictureTitle,
               Link = x.Link
           }).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }
    }
}
