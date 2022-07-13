using System;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class InventoryOperation
    {
        public long Id { get;private set; }
        public DateTime OperationDate { get;private set; }
        public long Count { get;private set; }
        public long InventoryId { get;private set; }
        public string Description { get;private set; }
        public long CurrentCount { get;private set; }
        public long OrderId { get;private set; }
        public long OperatorId { get;private set; }
        public bool Operation { get;private set; }
        public Inventory Inventory { get; set; }

        public InventoryOperation(long count, long inventoryId, string description, long currentCount, long orderId, long operatorId, bool operation)
        {
            Count = count;
            InventoryId = inventoryId;
            Description = description;
            CurrentCount = currentCount;
            OrderId = orderId;
            OperatorId = operatorId;
            Operation = operation;
            OperationDate = DateTime.Now;
        }
    }
}