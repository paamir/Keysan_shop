using System.Collections.Generic;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Application.Contracts.Product
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public string CategoryS { get; set; }
        public long CategoryId { get; set; }
        public string CreationDate { get; set; }
        public bool Stock { get; set; }
    }
}