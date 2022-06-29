using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Application.Contracts.Product
{
    public class ProductCreateModel
    {
        [Required(ErrorMessage = ValidationModel.IsRequired)]
        public string Name { get; set; }
        [Range(1, int.MaxValue)]
        public int Code { get; set; }
        [Required(ErrorMessage = ValidationModel.IsRequired)]
        public string Slug { get; set; }

        [Range(1,double.MaxValue)]
        public double UnitPrice { get; set; }
        public bool IsInStock { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        [Required(ErrorMessage = ValidationModel.IsRequired)]
        public string Description { get; set; }
        [Required(ErrorMessage = ValidationModel.IsRequired)]
        public string ShortDescription { get; set; }
        [Required(ErrorMessage = ValidationModel.IsRequired)]
        public string Keywords { get; set; }
        [Required(ErrorMessage = ValidationModel.IsRequired)]
        public string MetaDescription { get; set; }
        [Range(1, 100000)]
        public long CategoryId { get; set; }
        public List<ProductCategoryViewModel> Categories { get; set; }

    }
}
