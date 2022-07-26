using System.Collections.Generic;
using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductPicture;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository : IRepository<long, ProductPicture>
    {
        ProductPictureEditModel GetDetailBy(long id);
        List<ProductPictureViewModel> Search(ProductPictureSearchModel command);

    }
}
