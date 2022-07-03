using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework;
using _0_Framework.Domain;
using Domain.ProductCategoryAgg;
using Domain.ProductPictureAgg;

namespace Domain.ProductAgg
{
    public class Product : EntityBase
    {
        public string Name { get; private set; }
        public int Code { get; private set; }
        public double UnitPrice { get; private set; }
        public bool IsInStock { get; private set; }
        public string Slug { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Description { get; private set; }
        public string ShortDescription { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }
        public long CategoryId { get; private set; }
        public ProductCategory Category { get; private set; }
        public List<ProductPicture> ProductPictures { get; private set; }
        public Product()
        {
            Category = new ProductCategory();
            ProductPictures = new List<ProductPicture>();
        }
        public Product(string name, int code, double unitPrice, string picture, string pictureAlt, string pictureTitle,
            string description, string shortDescription, string keywords, string metaDescription, long categoryId, string slug)
        {
            Name = name;
            Code = code;
            UnitPrice = unitPrice;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Description = description;
            ShortDescription = shortDescription;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CategoryId = categoryId;
            Slug = slug;
            IsInStock = false;
        }
        public void Edit(string name, int code, double uintPrice, string picture, string pictureAlt, string pictureTitle,
            string description, string shortDescription, string keywords, string metaDescription, long categoryId,string slug)
        {
            Name = name;
            Code = code;
            UnitPrice = uintPrice;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Description = description;
            ShortDescription = shortDescription;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CategoryId = categoryId;
            Slug = slug;
        }

        public void InStock()
        {
            IsInStock = true;
        }
        public void NotInStock()
        {
            IsInStock = false;
        }
    }
}