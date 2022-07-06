using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscountManagement.Domain.CustomerDiscountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscountManagement.Infrastructure.EFCore.Mapping
{
    public class CustomerDiscountMapping : IEntityTypeConfiguration<CustomerDiscount>
    {
        public void Configure(EntityTypeBuilder<CustomerDiscount> builder)
        {
            builder.ToTable("Discounts");
            builder.HasKey(x => x.Id);


            builder.Property(x => x.Discount).IsRequired().HasMaxLength(100);
            builder.Property(x => x.EndDate).IsRequired();
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();


        }
    }
}