using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InventoryManagement.Application.Contract.Inventory
{
    public class InventoryReductionModel
    {
        public long InventoryId { get; set; }
        public long ProductId { get; set; }
        public long Count { get; set; }
        public long OperatorId { get; set; }
        public long OrderId { get; set; }
        public string Description { get; set; }
        public string Product { get; set; }
    }
}