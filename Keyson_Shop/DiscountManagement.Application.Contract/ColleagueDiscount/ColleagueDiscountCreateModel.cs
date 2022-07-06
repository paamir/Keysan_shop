using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;

namespace DiscountManagement.Application.Contract.ColleagueDiscount
{
    public class ColleagueDiscountCreateModel
    {
        [Range(1,100000, ErrorMessage = ValidationModel.IsRequired)]
        public long ProductId { get; set; }
        [Range(1, 100, ErrorMessage = ValidationModel.IsRequired)]
        public int Discount { get; set; }
        public List<ProductViewModel> Products { get; set; }

    }
}
