using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InventoryManagement.Application.Contract.Inventory
{
    public class InventoryReductionModel
    {
        [Range(1, 100000, ErrorMessage = ValidationModel.IsRequired)]
        public long InventoryId { get; set; }
        [Range(1, 100000, ErrorMessage = ValidationModel.IsRequired)]
        public long ProductId { get; set; }

        [Range(1, 100000, ErrorMessage = ValidationModel.IsRequired)]
        public long Count { get; set; }
        [Range(1, 100000, ErrorMessage = ValidationModel.IsRequired)]
        public long OperatorId { get; set; }
        [Range(1, 100000, ErrorMessage = ValidationModel.IsRequired)]
        public long OrderId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Product { get; set; }
    }
}