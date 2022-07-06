using System.Collections.Generic;
using _0_Framework.Application;

namespace DiscountManagement.Application.Contract.CustomerDiscount
{
    public interface ICustomerDiscountApplication
    {
        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel command);
        public CustomerDiscountEditModel GetDetailBy(long id);
        public OperationResult Edit(CustomerDiscountEditModel command);
        public OperationResult Create(CustomerDiscountCreateModel command);

    }
}