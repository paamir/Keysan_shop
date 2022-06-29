using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Application.Contracts.Product
{
    public interface IProductApplication
    {
        OperationResult Create(ProductCreateModel command);
        OperationResult Edit(ProductEditModel command);
        List<ProductViewModel> GetAll();
        OperationResult IsInStock(long id);
        OperationResult NotInStock(long id);
        ProductEditModel GetDetailBy(long id);
        List<ProductViewModel> Search(ProductSearchModel searchModel);
        List<ProductViewModel> GetProducts();
    }
}
