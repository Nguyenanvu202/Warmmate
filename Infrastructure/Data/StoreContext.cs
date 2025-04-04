using System;
using System.Net.Http.Headers;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Infrastructure.Config.ProductConfig;

using Infrastructure.Config.ProductConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Data;

public class StoreContext : IdentityDbContext<AppUser>
{
    public StoreContext(DbContextOptions<StoreContext> options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ItemImgConfig).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductCategoryConfig).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductItemConfig).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VariationConfig).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VariationOptConfig).Assembly);
    }
    protected StoreContext()
    {
    }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<ProductItem> ProductItems { get; set; }
    public DbSet<ProductItemImg> ProductItemImgs { get; set; }
    public DbSet<Variation> Variations { get; set; }
    public DbSet<VariationOpt> VariationOpts { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
}
