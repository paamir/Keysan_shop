using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _01_Keyson_Shop_Query.Contract.Product;
using _01_Keyson_Shop_Query.Contract.ProductPictures;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Infrastructure.EFCore;

namespace _01_Keyson_Shop_Query.Implementation
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _context;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;

        public ProductQuery(ShopContext context, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _context = context;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public List<ProductQueryModel> LatestArrivals()
        {
            var inventories =
                _inventoryContext.Inventories.Select(x => new {ProductId = x.ProductId, UnitPrice = x.UnitPrice});
            var discounts =
                _discountContext.CustomerDiscounts.Where(x => x.EndDate > DateTime.Now && x.StartDate < DateTime.Now)
                    .Select(x => new
                        {DiscountRate = x.Discount, ProductId = x.ProductId});
            var products = _context.Products.Select(x => new ProductQueryModel
            {
                Category = x.Category.Name,
                Name = x.Name,
                Id = x.Id,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            }).AsNoTracking().OrderByDescending(x => x.Id).Take(5).ToList();

            foreach (var product in products)
            {
                var Inventory = inventories.FirstOrDefault(x => x.ProductId == product.Id);
                var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                if (Inventory != null)
                {
                    var price = Inventory.UnitPrice;
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

            return products;
        }

        public List<ProductQueryModel> Search(string searchInput)
        {
            var inventories =
                _inventoryContext.Inventories.Select(x => new {ProductId = x.ProductId, Unitprice = x.UnitPrice,});

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && DateTime.Now < x.EndDate).Select(discount => new
                    {DiscountRate = discount.Discount, ProductId = discount.ProductId, EndDate = discount.EndDate});
            // implete is visible in productquery modle
            var products = _context.Products
                .Include(x => x.Category)
                .Select(product => new ProductQueryModel
                {
                    Id = product.Id,
                    Category = product.Category.Name,
                    Name = product.Name,
                    Picture = product.Picture,
                    PictureAlt = product.PictureAlt,
                    PictureTitle = product.PictureTitle,
                    Slug = product.Slug,
                    ShortDescription = product.ShortDescription,
                }).AsNoTracking();
            if (!string.IsNullOrWhiteSpace(searchInput))
                products = products.Where(x =>
                    x.Name.Contains(searchInput) || x.Category.Contains(searchInput) ||
                    x.ShortDescription.Contains(searchInput));

            foreach (var product in products)
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

            return products.ToList();
        }

        public ProductQueryModel FindProductBy(string slug)
        {
            var inventories =
                _inventoryContext.Inventories.Select(x => new
                    {ProductId = x.ProductId, Unitprice = x.UnitPrice, CurrentCount = x.CurrentStockCount()});
            var pictures = _context.ProductsPicture.Where(x => !x.IsDeleted).Select(x => new ProductPictureQueryModel
            {
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ProductId = x.ProductId
            });

        var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && DateTime.Now < x.EndDate).Select(discount => new
                { DiscountRate = discount.Discount, ProductId = discount.ProductId, EndDate = discount.EndDate });
            // implete is visible in productquery modle
            var product = _context.Products
                .Include(x => x.Category)
                .Select(product => new ProductQueryModel
                {
                    Id = product.Id,
                    Category = product.Category.Name,
                    Name = product.Name,
                    Picture = product.Picture,
                    PictureAlt = product.PictureAlt,
                    PictureTitle = product.PictureTitle,
                    Slug = product.Slug,
                    ShortDescription = product.ShortDescription,
                    Description = product.Description,
                    Code = product.Code
                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);

            if (product != null)
            {
                var Pictures = pictures.Where(x => x.ProductId == product.Id);
                var inventory = inventories.FirstOrDefault(x => x.ProductId == product.Id);
                var discount = discounts.FirstOrDefault(x1 => x1.ProductId == product.Id);

                if (Pictures != null)
                {
                    product.Pictures = Pictures.ToList();
                }
                if (inventory != null)
                {
                    product.InStock = inventory.CurrentCount > 0;
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

            return product;
        }
    }
}