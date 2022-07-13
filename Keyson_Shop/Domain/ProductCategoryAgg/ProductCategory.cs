using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework;
using _0_Framework.Domain;
using Domain.ProductAgg;
using ShopManagement.Domain.ProductAgg;

namespace Domain.ProductCategoryAgg
{
    public class ProductCategory : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Keywords { get; private set; }
        public string Slug { get; private set; }
        public string MetaDescription { get; private set; }
        public List<Product> Products { get; private set; }

        public ProductCategory()
        {
            Products = new List<Product>();
        }

        public ProductCategory(string name, string description, string picture, string pictureAlt,
            string pictureTitle, string keywords, string slug, string metaDescription)
        {
            Name = name;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keywords = keywords;
            Slug = slug;
            MetaDescription = metaDescription;
        }

        public void Edit(string name, string description, string picture, string pictureAlt,
            string pictureTitle, string keywords, string slug, string metaDescription)
        {
            Name = name;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keywords = keywords;
            Slug = slug;
            MetaDescription = metaDescription;
        }
    }
}