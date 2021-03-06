using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductPictureRepository : RepositoryBase<long, ProductPicture>, IProductPictureRepository
    {
        private readonly ShopContext _context;

        public ProductPictureRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public ProductPictureEditModel GetDetailBy(long id)
        {
            return _context.ProductsPicture.Select(x => new ProductPictureEditModel()
            {
                Id = x.Id,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ProductId = x.ProductId
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel command)
        {
            var products = _context.ProductsPicture
                .Include(x => x.Product)
                .Select(x => new ProductPictureViewModel
                {
                    CreationDate = x.CreationDate.ToFarsi(),
                    Id = x.Id,
                    Picture = x.Picture,
                    IsDeleted = x.IsDeleted,
                    Product = x.Product.Name,
                }).AsNoTracking();


            if (command.ProductId != 0)
            {
                products = products.Where(x => x.ProductId == command.ProductId);
            }



            return products.AsNoTracking().OrderByDescending(x => x.Id).ToList();
        }
    }
}