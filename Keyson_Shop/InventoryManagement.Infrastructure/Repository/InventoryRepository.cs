using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using ShopManagement.Infrastructure.EFCore;

namespace InventoryManagement.Infrastructure.EFCore.Repository
{
    public class InventoryRepository : RepositoryBase<long, Inventory>, IInventoryRepository
    {
        private readonly InventoryContext _context;
        private readonly ShopContext _shopContext;

        public InventoryRepository(InventoryContext context, ShopContext shopContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
        }

        public Inventory GetByP(long productId)
        {
            return _context.Inventories.FirstOrDefault(x => x.ProductId == productId);
        }

        public List<InventoryOperationViewModel> GetLogOperations(long inventoryId)
        {
            var Inventory = _context.Inventories.FirstOrDefault(x => x.Id == inventoryId);
            return Inventory.Operations.Select(x => new InventoryOperationViewModel
            {
                Count = x.Count,
                Description = x.Description,
                CurrentCount = x.CurrentCount,
                Id = x.Id,
                Operation = x.Operation,
                OperationDate = x.OperationDate.ToFarsi(),
                Operator = "مدیرسیستم",
                OrderId = x.OrderId,
                OperatorId = x.OperatorId
            }).ToList();
        }

        public List<InventoryViewModel> Search(InventorySearchModel command)
        {
            var Products = _shopContext.Products.Select(x => new {Id = x.Id, Name = x.Name}).ToList();
            var query = _context.Inventories.Select(x => new InventoryViewModel
            {
                CreationDate = x.CreationDate,
                ProductId = x.ProductId,
                Id = x.Id,
                IsInStock = x.IsInStock,
                UnitPrice = x.UnitPrice,
                CurrentCount = x.CurrentStockCount()
            });

            if (command.IsInStock)
            {
                query = query.Where(x => !x.IsInStock);
            }

            if (command.ProductId != 0)
            {
                query = query.Where(x => x.ProductId == command.ProductId);
            }

            var Inventories = query.OrderByDescending(x => x.Id).ToList();
            Inventories.ForEach(Inventory =>
                Inventory.Product = Products.FirstOrDefault(x => x.Id == Inventory.ProductId)?.Name);

            return Inventories;
        }

        public InventoryEditModel GetDetail(long id)
        {
            return _context.Inventories.Select(x => new InventoryEditModel
            {
                ProductId = x.ProductId,
                Id = x.Id,
                UnitPrice = x.UnitPrice
            }).FirstOrDefault(x => x.Id == id);
        }
    }
}