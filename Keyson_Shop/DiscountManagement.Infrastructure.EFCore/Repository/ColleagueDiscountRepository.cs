using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class ColleagueDiscountRepository : RepositoryBase<long, ColleagueDiscount>, IColleagueDiscountRepository
    {
        private readonly DiscountContext _context;
        private readonly ShopContext shopContext;

        public ColleagueDiscountRepository(DiscountContext context, ShopContext shopContext) : base(context)
        {
            _context = context;
            this.shopContext = shopContext;
        }

        public ColleagueDiscountEditModel GetDetail(long id)
        {
            return _context.ColleagueDiscounts.Select(x => new ColleagueDiscountEditModel()
            {
                Discount = x.Discount,
                Id = x.Id,
                ProductId = x.ProductId
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel command)
        {
            var products = shopContext.Products.Select(x => new {id = x.Id, name = x.Name}).ToList();
            var query = _context.ColleagueDiscounts.Select(x => new ColleagueDiscountViewModel()
            {
                Discount = x.Discount,
                Id = x.Id,
                IsDeleted = x.IsDeleted,
                ProductId = x.ProductId,
                CreationDate = x.CreationDate
            }).AsNoTracking();
            if (command.ProductId != 0)
            {
                query = query.Where(x => x.ProductId == command.ProductId).AsNoTracking();
            }

            var discounts = query.OrderByDescending(x => x.Id).ToList();
            discounts.ForEach(discount =>
                discount.Product = products.FirstOrDefault(x => x.id == discount.ProductId)?.name);
            return discounts;

        }
    }
}