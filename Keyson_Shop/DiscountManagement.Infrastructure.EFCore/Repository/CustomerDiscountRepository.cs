using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class CustomerDiscountRepository : RepositoryBase<long, CustomerDiscount>, ICustomerDiscountRepository
    {
        private readonly DiscountContext _context;
        private readonly ShopContext _shopContext;

        public CustomerDiscountRepository(DiscountContext context, ShopContext shopContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel command)
        {
            var Products = _shopContext.Products.Select(x => new {Id = x.Id, Name = x.Name});

        var query = _context.CustomerDiscounts
                .Select(x => new CustomerDiscountViewModel
                {
                    Discount = x.Discount,
                    EndDate = x.EndDate,
                    StartDate = x.StartDate,
                    StartDateS = x.StartDate.ToString(),
                    EndDateS = x.StartDate.ToString(),
                    ProductId = x.ProductId,
                    Reason = x.Reason
                });
            if (command.Discount != 0)
            {
                query = query.Where(x => x.Discount == command.Discount);
            }
            if (command.ProductId != 0)
            {
                query = query.Where(x => x.ProductId == command.ProductId);
            }
            if (!string.IsNullOrWhiteSpace(command.Reason))
            {
                query = query.Where(x => x.Reason.Contains(command.Reason));
            }
            if (!string.IsNullOrWhiteSpace(command.StartDateS))
            {
                var startDate = DateTime.Now;
                query = query.Where(x => x.StartDate < startDate);
            }
            if (!string.IsNullOrWhiteSpace(command.EndDateS))
            {
                var endDate = DateTime.Now;

                query = query.Where(x => x.EndDate > endDate);
            }

 
            var discounts = query.OrderByDescending(x => x.Id).ToList();

            discounts
                .ForEach(discount => discount.Product = Products.FirstOrDefault(x => x.Id == discount.ProductId)?.Name);

            return discounts;
        }

        public CustomerDiscountEditModel GetDetail(long id)
        {
           return _context.CustomerDiscounts.Select(x => new CustomerDiscountEditModel
           {
               Discount = x.Discount,
               EndDate = x.EndDate,
               Id = x.Id,
               ProductId = x.ProductId,
               Reason = x.Reason,
               StartDate = x.StartDate
           }).FirstOrDefault(x => x.Id == id);
        }
    }
}