using System.Collections;
using System.Collections.Generic;
using _0_Framework;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.ProductCategory
{
    public interface IProductCategoryApplication
    {
        OperationResult Create(CreateProductCategoryModel command);
        OperationResult Edit(EditProductCategoryModel command);
        OperationResult unVisible(long id);
        OperationResult Visible(long id);

        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
        EditProductCategoryModel GetDetail(long id);
        List<ProductCategoryViewModel> GetProductCategories();
    }
}