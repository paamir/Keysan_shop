using System;
using _0_Framework.Domain;

namespace DiscountManagement.Domain.CustomerDiscountAgg
{
    public class CustomerDiscount : EntityBase
    {
        public long ProductId { get;private set; }
        public DateTime StartDate { get;private set; }
        public DateTime EndDate { get;private set; }
        public int Discount { get;private set; }
        public string Reason { get;private set; }

        public CustomerDiscount(long productId, DateTime startDate, DateTime endDate, int discount, string reason)
        {
            ProductId = productId;
            StartDate = startDate;
            EndDate = endDate;
            Discount = discount;
            Reason = reason;
        }
        public void Edit(long productId, DateTime startDate, DateTime endDate, int discount, string reason)
        {
            ProductId = productId;
            StartDate = startDate;
            EndDate = endDate;
            Discount = discount;
            Reason = reason;
        }


    }
}
