using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using Domain.ProductPictureAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductPicture;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductPictureRepository : RepositoryBase<long, ProductPicture>, IProductPictureRepository
    {
        private readonly ShopContext _context;

        public ProductPictureRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public ProductPictureEditModel GetDetailBy(int id)
        {
            return _context.ProductsPicture.Select(x => new ProductPictureEditModel()
            {
                Id = x.Id,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ProductId = x.ProductId
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel command)
        {
            var products = _context.ProductsPicture
                .Include(x => x.Product)
                .Select(x => new ProductPictureViewModel
                {
                    CreationDate = x.CreationDate.ToString(),
                    Id = x.Id,
                    Picture = x.Picture,
                    IsDeleted = x.IsDeleted,
                    Product = x.Product.Name,
                });


            if (command.ProductId != 0)
            {
                products = products.Where(x => x.ProductId == command.ProductId);
            }

            

            return products.OrderByDescending(x => x.Id).ToList();
        }
    }
}