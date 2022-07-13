﻿using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.EFCore.Mapping;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;

namespace InventoryManagement.Infrastructure.EFCore
{
    public class InventoryContext : DbContext
    {
        public DbSet<Inventory> Inventories { get; set; }
        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(InventoryMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
