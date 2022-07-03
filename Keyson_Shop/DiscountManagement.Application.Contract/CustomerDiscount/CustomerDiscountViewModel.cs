using System;

namespace DiscountManagement.Application.Contract.CustomerDiscount
{
    public class CustomerDiscountViewModel
    {
        public long ProductId { get; set; }
        public string Product { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Discount { get; set; }
        public string Reason { get; set; }
        public long Id { get; set; }
        public string StartDateS { get; set; }
        public string EndDateS { get; set; }
    }
}