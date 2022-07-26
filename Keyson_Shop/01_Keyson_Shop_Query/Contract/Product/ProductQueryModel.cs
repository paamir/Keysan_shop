using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Keyson_Shop_Query.Contract.ProductPictures;
using ShopManagement.Domain.ProductPictureAgg;

namespace _01_Keyson_Shop_Query.Contract.Product
{
    public class ProductQueryModel
    {
        public long Id { get; set; }
        public int DiscountRate { get; set; }
        public string UnitPrice { get; set; }
        public string UnitPriceAfterDiscount { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string SecondPicture { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public bool HasDiscount { get; set; }
        public string DiscountEndDate { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public bool InStock { get; set; }
        public List<ProductPictureQueryModel> Pictures { get; set; }
        public long Code { get; set; }
    }
}
