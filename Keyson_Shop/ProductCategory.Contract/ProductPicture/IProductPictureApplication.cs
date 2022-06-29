using System.Collections.Generic;
using _0_Framework;
using Domain.ProductPictureAgg;

namespace ShopManagement.Application.Contracts.ProductPicture
{
    public interface IProductPictureApplication
    {
        OperationResult Create(ProductPictureCreateModel command);
        OperationResult Edit(ProductPictureEditModel command);
        OperationResult Delete(long id);
        OperationResult Restore(long id);
        List<ProductPictureViewModel> Search(ProductPictureSearchModel command);
        ProductPictureEditModel GetDetailBy(int id);
    }
}