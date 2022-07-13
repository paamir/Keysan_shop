using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework;
using _0_Framework.Domain;
using Domain.ProductAgg;
using ShopManagement.Domain.ProductAgg;

namespace Domain.ProductPictureAgg
{
    public class ProductPicture : EntityBase
    {
        public long ProductId{ get;private set; }
        public string Picture { get;private set; }
        public string PictureAlt { get;private set; }
        public string PictureTitle { get;private set; }
        public bool IsDeleted { get;private set; }
        public Product Product { get;private set; }

        public ProductPicture()
        {
            Product = new Product();
        }

        public ProductPicture(long productId, string picture, string pictureAlt, string pictureTitle)
        {
            ProductId = productId;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            IsDeleted = false;
        }
        public void Edit(long productId, string picture, string pictureAlt, string pictureTitle)
        {
            ProductId = productId;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
        }

        public void Delete()
        {
            IsDeleted = true;
        }
        public void Restore()
        {
            IsDeleted = false;
        }
    }
}
