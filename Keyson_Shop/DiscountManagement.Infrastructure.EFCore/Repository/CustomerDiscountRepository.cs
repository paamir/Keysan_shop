using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
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
                    Id = x.Id,
                    Discount = x.Discount,
                    EndDate = x.EndDate,
                    StartDate = x.StartDate,
                    StartDateS = x.StartDate.ToFarsi(),
                    EndDateS = x.EndDate.ToFarsi(),
                    ProductId = x.ProductId,
                    Reason = x.Reason
                }).AsNoTracking();
            if (command.Discount != 0)
            {
                query = query.Where(x => x.Discount == command.Discount);
            }
            if (command.ProductId != 0)
            {
                query = query.Where(x => x.ProductId == command.ProductId);
            }
            if (!string.IsNullOrWhiteSpace(command.StartDateS))
            {
                var startDate = command.StartDateS.ToGeorgianDateTime();
                query = query.Where(x => x.StartDate >= startDate);
            }
            if (!string.IsNullOrWhiteSpace(command.EndDateS))
            {
                var endDate = command.EndDateS.ToGeorgianDateTime();

                query = query.Where(x => x.EndDate <= endDate);
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
               EndDateS = x.EndDate.ToString(),
               Id = x.Id,
               ProductId = x.ProductId,
               Reason = x.Reason,
               StartDateS = x.StartDate.ToString()
           }).FirstOrDefault(x => x.Id == id);
        }
    }
}