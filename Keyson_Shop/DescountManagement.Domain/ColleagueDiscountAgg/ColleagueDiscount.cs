using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using Domain.ProductAgg;

namespace DiscountManagement.Domain.ColleagueDiscountAgg
{
    public class ColleagueDiscount : EntityBase
    {
        public long ProductId { get;private set; }
        public int Discount { get;private set; }
        public bool IsDeleted { get;private set; }

        public ColleagueDiscount(long productId, int discount)
        {
            ProductId = productId;
            Discount = discount;
            IsDeleted = false;
        }

        public void Edit(long productId, int discount)
        {
            ProductId = productId;
            Discount = discount;
        }

        public void Delete()
        {
            IsDeleted = true;
        }

        public void Restore()
        {
            IsDeleted = false;
        }
    }
}
