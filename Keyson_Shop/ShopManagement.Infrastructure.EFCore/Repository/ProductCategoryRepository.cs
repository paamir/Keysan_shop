using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

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
        }).AsNoTracking().FirstOrDefault();
    }

    public List<ProductCategoryViewModel> Search(ProductCategorySearchModel productCategorySearch)
    {
        var Products = _context.ProductCategories.Select(x => new ProductCategoryViewModel()
        {
            Id = x.Id,
            Description = x.Description,
            Name = x.Name,
            Picture = x.Picture,
            CreationDate = x.CreationDate.ToFarsi(),
            IsVisible = x.IsVisible
        }).AsNoTracking();

        if (!string.IsNullOrWhiteSpace(productCategorySearch.Name))
        {
            Products = Products.Where(x => x.Name == productCategorySearch.Name);
        }

        return Products.OrderByDescending(x => x.Id).ToList();
    }

    public List<ProductCategoryViewModel> GetProductCategories()
    {
        return _context.ProductCategories.Select(x => new ProductCategoryViewModel()
        {
            Id = x.Id,
            Name = x.Name
        }).AsNoTracking().ToList();
    }
    }
}
