using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Keyson_Shop_Query.Contract.Product
{
    public interface IProductQuery
    {
        List<ProductQueryModel> LatestArrivals();
        List<ProductQueryModel> Search(string searchInput);
        ProductQueryModel FindProductBy(string slug);
    }
}
