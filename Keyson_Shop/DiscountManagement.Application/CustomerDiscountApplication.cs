using System;
using System.Collections.Generic;
using _0_Framework.Application;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;

namespace DiscountManagement.Application
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _customerDiscountRepository;

        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
        {
            _customerDiscountRepository = customerDiscountRepository;
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel command)
        {
            return _customerDiscountRepository.Search(command);
        }

        public CustomerDiscountEditModel GetDetailBy(long id)
        {
            return _customerDiscountRepository.GetDetail(id);
        }

        public OperationResult Edit(CustomerDiscountEditModel command)
        {
            var operationResult = new OperationResult();
            var Discount = _customerDiscountRepository.GetBy(command.Id);
            var startDate = command.StartDateS.ToGeorgianDateTime();
            var endDate = command.EndDateS.ToGeorgianDateTime();
            if (Discount == null)
            {
                return operationResult.Failed(OperationMessages.RecordNotFound);
            }

            Discount.Edit(command.ProductId, startDate, endDate, command.Discount, command.Reason);
            _customerDiscountRepository.SaveChanges();
            return operationResult.Succdded();
        }

        public OperationResult Create(CustomerDiscountCreateModel command)
        {
            var operationResult = new OperationResult();
            var startDate = command.StartDateS.ToGeorgianDateTime();
            var endDate = command.EndDateS.ToGeorgianDateTime();
            if (_customerDiscountRepository.Exists(x =>
                    x.ProductId == command.ProductId && x.Discount == command.Discount &&
                    x.StartDate == startDate))
            {
                return operationResult.Failed(OperationMessages.Duplicate);
            }

            _customerDiscountRepository.Create(new CustomerDiscount(command.ProductId, startDate,
                endDate, command.Discount, command.Reason));
            _customerDiscountRepository.SaveChanges();
            return operationResult.Succdded();
        }
    }
}