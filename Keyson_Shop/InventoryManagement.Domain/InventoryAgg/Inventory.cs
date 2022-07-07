using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Domain;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class Inventory : EntityBase
    {
        public long ProductId { get;private set; }
        public bool IsInStock { get;private set; }
        public double UnitPrice { get;private set; }
        public List<InventoryOperation> Operations { get;private set; }
        public Inventory(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            IsInStock = false;
        }

        public long CurrentStockCount()
        {
            var plus = Operations.Where(x => x.Operation).Sum(x => x.Count);
            var minus = Operations.Where(x => !x.Operation).Sum(x => x.Count);
            return plus - minus;
        }

        public void Increase(long count, string description, long operatorId)
        {
            var currentCount = CurrentStockCount() + count;
            var inventoryOperation =
                new InventoryOperation(count, this.Id, description, currentCount, 0, operatorId, true);
            Operations.Add(inventoryOperation);
            this.IsInStock = currentCount > 0;
        }
        public void Reduction(long count, string description, long operatorId,long orderId)
        {
            var currentCount = CurrentStockCount() - count;
            var inventoryOperation =
                new InventoryOperation(count, this.Id, description, currentCount, orderId, operatorId, false);
            Operations.Add(inventoryOperation);
            this.IsInStock = currentCount > 0;
        }

        public void Edit(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
        }
    }
}
