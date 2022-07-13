using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopManagement.Application.Contracts.Product;

namespace InventoryManagement.Application.Contract.Inventory
{
    public class InventoryCreateModel
    {
        public long ProductId { get; set; }
        public double UnitPrice { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
