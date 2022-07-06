using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using Domain.ProductAgg;
using ShopManagement.Application.Contracts.Product;

namespace DiscountManagement.Application.Contract.CustomerDiscount
{
    public class CustomerDiscountCreateModel
    {
        [Range(1,100000, ErrorMessage = ValidationModel.IsRequired)]  
        public long ProductId { get; set; }
        [Range(1, 100, ErrorMessage = ValidationModel.IsRequired)]
        public int Discount { get; set; }
        [Required(ErrorMessage = ValidationModel.IsRequired)]
        public string Reason { get; set; }
        public List<ProductViewModel> Products { get; set; }
        [Required(ErrorMessage = ValidationModel.IsRequired)]
        public string StartDateS { get; set; }
        [Required(ErrorMessage = ValidationModel.IsRequired)]
        public string EndDateS { get; set; }

    }
}
