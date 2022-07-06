using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;

namespace DiscountManagement.Application
{
    public class ColleagueDiscountApplication : IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository _colleagueDiscountRepository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository)
        {
            _colleagueDiscountRepository = colleagueDiscountRepository;
        }

        public ColleagueDiscountEditModel GetDetailBy(long id)
        {
            return _colleagueDiscountRepository.GetDetail(id);
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel command)
        {
            return _colleagueDiscountRepository.Search(command);
        }

        public OperationResult Edit(ColleagueDiscountEditModel command)
        {
            var operationResult = new OperationResult();
            var discount = _colleagueDiscountRepository.GetBy(command.Id);

            if (discount == null)
                return operationResult.Failed(OperationMessages.RecordNotFound);
            if (_colleagueDiscountRepository.Exists(x =>
                    x.ProductId == command.ProductId && x.Discount == command.Discount))
                return operationResult.Failed(OperationMessages.Duplicate);

            discount.Edit(command.Id, command.Discount);
            _colleagueDiscountRepository.SaveChanges();
            return operationResult.Succdded();
        }

        public OperationResult Create(ColleagueDiscountCreateModel command)
        {
            var operationResult = new OperationResult();

            if (_colleagueDiscountRepository.Exists(x =>
                    x.ProductId == command.ProductId && x.Discount == command.Discount))
                return operationResult.Failed(OperationMessages.Duplicate);

            _colleagueDiscountRepository.Create(new ColleagueDiscount(command.ProductId, command.Discount));
            _colleagueDiscountRepository.SaveChanges();
            return operationResult.Succdded();
        }

        public OperationResult Delete(long id)
        {
            var operationResult = new OperationResult();
            var discount = _colleagueDiscountRepository.GetBy(id);

            if (discount == null)
                return operationResult.Failed(OperationMessages.RecordNotFound);

            discount.Delete();
            _colleagueDiscountRepository.SaveChanges();
            return operationResult.Succdded();
        }

        public OperationResult Restore(long id)
        {
            var operationResult = new OperationResult();
            var discount = _colleagueDiscountRepository.GetBy(id);

            if (discount == null)
                return operationResult.Failed(OperationMessages.RecordNotFound);

            discount.Restore();
            _colleagueDiscountRepository.SaveChanges();
            return operationResult.Succdded();
        }
    }
}
