using System.Collections;
using System.Collections.Generic;
using _0_Framework;

namespace ShopManagement.Application.Contracts.ProductCategory
{
    public interface IProductCategoryApplication
    {
        OperationResult Create(CreateProductCategoryModel command);
        OperationResult Edit(EditProductCategoryModel command);
        List<ProductCategoryViewModel> GetAll();
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
        EditProductCategoryModel GetDetail(long id);
        List<ProductCategoryViewModel> GetProductCategories();
    }
}