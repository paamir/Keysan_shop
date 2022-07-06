using System;
using System.Collections.Generic;
using ShopManagement.Application.Contracts.Product;

namespace DiscountManagement.Application.Contract.ColleagueDiscount
{
    public class ColleagueDiscountViewModel
    {
        public long Id { get; set; }
        public string Product { get; set; }
        public long Discount { get; set; }
        public bool  IsDeleted{ get; set; }
        public long ProductId { get; set; }
        public DateTime CreationDate { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}