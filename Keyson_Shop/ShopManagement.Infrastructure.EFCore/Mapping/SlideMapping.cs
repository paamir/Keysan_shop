using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class SlideMapping : IEntityTypeConfiguration<Slide>
    {
        public void Configure(EntityTypeBuilder<Slide> builder)
        {
            builder.ToTable("Slides");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).IsRequired().HasMaxLength(55);
            builder.Property(x => x.Text).IsRequired();
            builder.Property(x => x.ButtonText).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Header).HasMaxLength(40);
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.Picture).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.PictureAlt).IsRequired().HasMaxLength(255);
            builder.Property(x => x.PictureTitle).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Link).IsRequired();

        }
    }
}
