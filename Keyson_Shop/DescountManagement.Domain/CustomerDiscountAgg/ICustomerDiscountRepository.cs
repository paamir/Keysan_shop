using System.Collections.Generic;
using _0_Framework.Domain;
using DiscountManagement.Application.Contract.CustomerDiscount;

namespace DiscountManagement.Domain.CustomerDiscountAgg
{
    public interface ICustomerDiscountRepository : IRepository<long, CustomerDiscount>
    {
        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel command);
        public CustomerDiscountEditModel GetDetail(long id);
    }
}