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

        public CustomerDiscountEditModel GetDetail(long id)
        {
            return _customerDiscountRepository.GetDetail(id);
        }

        public OperationResult Edit(CustomerDiscountEditModel command)
        {
            var operationResult = new OperationResult();
            var Discount = _customerDiscountRepository.GetBy(command.Id);

            if (Discount == null)
            {
                return operationResult.Failed(OperationMessages.RecordNotFound);
            }

            if (_customerDiscountRepository.Exists(x => x.ProductId == command.ProductId))
            {
                return operationResult.Failed(OperationMessages.Duplicate);
            }

            Discount.Edit(command.ProductId, command.StartDate, command.EndDate, command.Discount, command.Reason);
            _customerDiscountRepository.SaveChanges();
            return operationResult.Succdded();
        }

        public OperationResult Create(CustomerDiscountCreateModel command)
        {
            var operationResult = new OperationResult();

            if (_customerDiscountRepository.Exists(x => x.ProductId == command.ProductId))
            {
                return operationResult.Failed(OperationMessages.Duplicate);
            }

            _customerDiscountRepository.Create(new CustomerDiscount(command.ProductId, command.StartDate,
                command.EndDate, command.Discount, command.Reason));
            _customerDiscountRepository.SaveChanges();
            return operationResult.Succdded();
        }
    }
}