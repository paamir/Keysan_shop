using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _01_Keyson_Shop_Query.Contract.Product;
using _01_Keyson_Shop_Query.Contract.ProductCategory;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore;

namespace _01_Keyson_Shop_Query.Implementation
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _context;
        private readonly InventoryContext _inventoryContext;
        public readonly DiscountContext _discountContext;

        public ProductCategoryQuery(ShopContext context, InventoryContext inventoryContext,
            DiscountContext discountContext)
        {
            _context = context;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public List<ProductCategoryQueryModel> GetProductCategories()
        {
            return _context.ProductCategories.Where(x => x.IsVisible == true).Select(x => new ProductCategoryQueryModel
            {
                Description = x.Description,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            }).AsNoTracking().ToList();
        }

        public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
        {
            var inventories =
                _inventoryContext.Inventories.Select(x => new {ProductId = x.ProductId, Unitprice = x.UnitPrice});

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && DateTime.Now < x.EndDate).Select(discount => new
                    {DiscountRate = discount.Discount, ProductId = discount.ProductId});

            var productCategories = _context.ProductCategories.Where(x => x.IsVisible == true)
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Products = MapProducts(x.Products)
                }).AsNoTracking().ToList();


            foreach (var productCategory in productCategories)
            {
                foreach (var product in productCategory.Products)
                {
                    var inventory = inventories.FirstOrDefault(x => x.ProductId == product.Id);
                    var discount = discounts.FirstOrDefault(x1 => x1.ProductId == product.Id);
                    if (inventory != null)
                    {
                        var price = inventory.Unitprice;
                        product.UnitPrice = price.ToMoney();
                        if (discount != null)
                        {
                            product.DiscountRate = discount.DiscountRate;
                            product.HasDiscount = discount.DiscountRate > 0;
                            product.UnitPriceAfterDiscount =
                                Math.Round(price - discount.DiscountRate * price / 100).ToMoney();
                        }
                    }
                }
            }

            return productCategories;
        }

        public ProductCategoryQueryModel GetProductCategoriesWithProducts(string slug)
        {
            var inventories =
                _inventoryContext.Inventories.Select(x => new {ProductId = x.ProductId, Unitprice = x.UnitPrice});

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && DateTime.Now < x.EndDate).Select(discount => new
                    {DiscountRate = discount.Discount, ProductId = discount.ProductId, EndDate = discount.EndDate});

            var productCategories = _context.ProductCategories.Where(x => x.IsVisible == true)
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Products = MapProducts(x.Products),
                    Slug = x.Slug
                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);

            foreach (var product in productCategories.Products)
            {
                var inventory = inventories.FirstOrDefault(x => x.ProductId == product.Id);
                var discount = discounts.FirstOrDefault(x1 => x1.ProductId == product.Id);
                if (inventory != null)
                {
                    var price = inventory.Unitprice;
                    product.UnitPrice = price.ToMoney();
                    if (discount != null)
                    {
                        product.DiscountRate = discount.DiscountRate;
                        product.HasDiscount = discount.DiscountRate > 0;
                        product.UnitPriceAfterDiscount =
                            Math.Round(price - discount.DiscountRate * price / 100).ToMoney();
                        product.DiscountEndDate = discount.EndDate.ToDiscountFormat();
                    }
                }
            }

            return productCategories;
        }

        private static List<ProductQueryModel> MapProducts(IEnumerable<Product> Products)
        {
            return Products.Select(product => new ProductQueryModel
            {
                Id = product.Id,
                Category = product.Category.Name,
                Name = product.Name,
                Picture = product.Picture,
                PictureAlt = product.PictureAlt,
                PictureTitle = product.PictureTitle,
                Slug = product.Slug
            }).ToList();
        }
    }
}