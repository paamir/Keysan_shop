using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Keyson_Shop_Query.Contract.Product;

namespace _01_Keyson_Shop_Query.Contract.ProductCategory
{
    public class ProductCategoryQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Keywords { get; set; }
        public string Slug { get; set; }
        public string MetaDescription { get; set; }
        public List<ProductQueryModel> Products { get; set; }
    }

}
