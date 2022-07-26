using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace InventoryManagement.Application.Contract.Inventory
{
    public class InventoryIncreaseModel
    {
        [Range(1, 100000, ErrorMessage = ValidationModel.IsRequired)]
        public long InventoryId { get; set; }
        [Range(1, long.MaxValue, ErrorMessage = ValidationModel.IsRequired)]
        public long Count { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public long OperatorId { get; set; }
    }
}