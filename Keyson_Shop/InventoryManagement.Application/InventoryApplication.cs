using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore.Storage;

namespace InventoryManagement.Application
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryApplication(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public OperationResult Create(InventoryCreateModel command)
        {
            var operationResult = new OperationResult();
            if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId))
            {
                return operationResult.Failed(OperationMessages.Duplicate);
            }

            _inventoryRepository.Create(new Inventory(command.ProductId, command.UnitPrice));
            _inventoryRepository.SaveChanges();

            return operationResult.Succdded();
        }

        public OperationResult Edit(InventoryEditModel command)
        {
            var operationResult = new OperationResult();
            var inventory = _inventoryRepository.GetBy(command.Id);
            if (inventory == null)
            {
                return operationResult.Failed(OperationMessages.RecordNotFound);
            }

            if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId && x.Id != command.Id))
            {
                return operationResult.Failed(OperationMessages.Duplicate);
            }


            inventory.Edit(command.ProductId, command.UnitPrice);
            _inventoryRepository.SaveChanges();

            return operationResult.Succdded();
        }

        public OperationResult Increase(InventoryIncreaseModel command)
        {
            var operationResult = new OperationResult();
            var inventory = _inventoryRepository.GetBy(command.InventoryId);
            if (inventory == null)
                return operationResult.Failed(OperationMessages.RecordNotFound);

            const long operationId = 1;
            inventory.Increase(command.Count, command.Description, operationId);
            _inventoryRepository.SaveChanges();

            return operationResult.Succdded();
        }

        public OperationResult Reduction(InventoryReductionModel command)
        {
            var operationResult = new OperationResult();
            var inventory = _inventoryRepository.GetBy(command.ProductId);
            if (inventory == null)
                return operationResult.Failed(OperationMessages.RecordNotFound);
            if (inventory.CurrentStockCount() - command.Count < 0)
            {
                return operationResult.Failed("امکان ثبت سفارش بیشتر از تعداد موجود نیست");
            }
            const long operatorId = 1;
            inventory.Reduction(command.Count, command.Description, operatorId,0);
            _inventoryRepository.SaveChanges();

            return operationResult.Succdded();
        }
        public OperationResult Reduction(List<InventoryReductionModel> command)
        {
            var operationResult = new OperationResult();
            const long operatorId = 1;
            foreach (var item in command)
            {
                var inventory = _inventoryRepository.GetByP(item.ProductId);
                if (inventory == null)
                    return operationResult.Failed(OperationMessages.RecordNotFound);
                if (inventory.CurrentStockCount() - item.Count < 0)
                {
                    return operationResult.Failed(
                        $"امکان ثبت سفارش از محصول {item.Product} بیشتر از تعداد موجود وجود ندارد");
                }

                inventory.Reduction(item.Count, item.Description, operatorId, 0);
            }

            _inventoryRepository.SaveChanges();
            return operationResult.Succdded();
        }

        public List<InventoryViewModel> Search(InventorySearchModel command)
        {
            return _inventoryRepository.Search(command);
        }

        public InventoryEditModel GetDetail(long id)
        {
            return _inventoryRepository.GetDetail(id);
        }
    }
}