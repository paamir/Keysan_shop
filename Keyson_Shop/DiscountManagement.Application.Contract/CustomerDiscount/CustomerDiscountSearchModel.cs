using System;

namespace DiscountManagement.Application.Contract.CustomerDiscount
{
    public class CustomerDiscountSearchModel
    {
        public long ProductId { get; set; }
        public int Discount { get; set; }
        public string StartDateS { get; set; }
        public string EndDateS { get; set; }
    }
}