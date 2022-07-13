using InventoryManagement.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infrastructure.EFCore.Mapping
{
    public class InventoryMapping : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("Inventory");
            builder.HasKey(x => x.Id);

            builder.OwnsMany(x => x.Operations, modelBuilder =>
            {
                modelBuilder.ToTable("InventoryOperations");
                modelBuilder.HasKey(x => x.Id);

                modelBuilder.Property(x => x.Description).HasMaxLength(1000).IsRequired();
                modelBuilder.Property(x => x.InventoryId).IsRequired();
                modelBuilder.Property(x => x.Count).IsRequired();
                modelBuilder.Property(x => x.CurrentCount).IsRequired();
                modelBuilder.Property(x => x.Operation).IsRequired();
                modelBuilder.Property(x => x.OperationDate).IsRequired();
                modelBuilder.Property(x => x.OperatorId).IsRequired();
                modelBuilder.Property(x => x.OrderId).IsRequired();
                modelBuilder.WithOwner(x => x.Inventory).HasForeignKey(x => x.InventoryId);
            });

            builder.Property(x => x.UnitPrice).IsRequired();
            builder.Property(x => x.IsInStock).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.CreationDate).IsRequired();

        }
    }
}
