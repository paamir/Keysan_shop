using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using Domain.ProductCategoryAgg;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductCategoryRepository: RepositoryBase<long, ProductCategory> , IProductCategoryRepository
    {
    private readonly ShopContext _context;

    public ProductCategoryRepository(ShopContext context) : base(context)
    {
        _context = context;
    }

    public EditProductCategoryModel GetDetailBy(long id)
    {
        return _context.ProductCategories.Where(x => x.Id == id).Select(x => new EditProductCategoryModel()
        {
            Name = x.Name,
            Description = x.Description,
            Id = x.Id,
            Keywords = x.Keywords,
            MetaDescription = x.MetaDescription,
            Picture = x.Picture,
            PictureTitle = x.PictureTitle,
            Slug = x.Slug,
            PictureAlt = x.PictureAlt
        }).FirstOrDefault();
    }
    public List<ProductCategoryViewModel> Search(ProductCategorySearchModel productCategorySearch)
    {
        var Products = _context.ProductCategories.Select(x => new ProductCategoryViewModel()
        {
            Id = x.Id,
            Description = x.Description,
            Name = x.Name,
            Picture = x.Picture,
            CreationDate = x.CreationDate.ToShortDateString()
        });

        if (!string.IsNullOrWhiteSpace(productCategorySearch.Name))
        {
            Products = Products.Where(x => x.Name == productCategorySearch.Name);
        }

        return Products.OrderByDescending(x => x.Id).ToList();
    }
    }
}
