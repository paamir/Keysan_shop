using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using Domain.ProductAgg;
using Domain.ProductCategoryAgg;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductPicture;

namespace Domain.ProductPictureAgg
{
    public interface IProductPictureRepository : IRepository<long, ProductPicture>
    {
        ProductPictureEditModel GetDetailBy(long id);
        List<ProductPictureViewModel> Search(ProductPictureSearchModel command);

    }
}
