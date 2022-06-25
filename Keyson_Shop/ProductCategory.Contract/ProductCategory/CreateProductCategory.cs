using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using Microsoft.EntityFrameworkCore;

namespace ShopManagement.Application.Contracts.ProductCategory
{
    public class CreateProductCategoryModel
    {
        [Required(ErrorMessage = ValidationModel.IsRequired)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        [Required(ErrorMessage = ValidationModel.IsRequired)]
        public string Keywords { get; set; }
        [Required(ErrorMessage = ValidationModel.IsRequired)]
        public string Slug { get; set; }
        [Required(ErrorMessage = ValidationModel.IsRequired)]
        public string MetaDescription { get; set; }
    }
}
