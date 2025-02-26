using System;
using System.Net.Http.Headers;
using Core.Entities;
using Infrastructure.Config.ProductConfig;
using Infrastructure.Config.ProductConfiguration;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Data;

public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
}
