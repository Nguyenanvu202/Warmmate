using System;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config.ProductConfiguration;

public class ProductItemConfig : IEntityTypeConfiguration<ProductItem>
{
    public void Configure(EntityTypeBuilder<ProductItem> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
        builder.Property(q => q.Quantity).IsRequired();
        builder.Property(n => n.Name).IsRequired().HasMaxLength(255);
        builder.Property(n => n.Description).HasMaxLength(500);

        builder.Property(p => p.CreatedBy).HasMaxLength(255);
		builder.Property(p => p.CreatedDate);
		builder.Property(p => p.UpdatedBy).HasMaxLength(255);
		builder.Property(p => p.UpdatedDate); 
         //Relation with ProductCategory
        builder
        .HasOne(p => p.ProductCategory)
        .WithMany(p => p.ProductItems)
        .HasForeignKey(i => i.ProductCategoryId);
        //Relation with ProductImgs
        builder
        .HasMany(i => i.ProductItemImgs)
        .WithOne(i => i.ProductItem)
        .HasForeignKey(i => i.ProductItemId);
        //Many to many relation
        builder
        .HasMany(e => e.VariationOpts)
        .WithMany(e => e.ProductItems)
        .UsingEntity(
            l => l.HasOne(typeof(VariationOpt)).WithMany().HasForeignKey("OptForeignKey").OnDelete(DeleteBehavior.Restrict),
            r => r.HasOne(typeof(ProductItem)).WithMany().HasForeignKey("ItemForeignKey").OnDelete(DeleteBehavior.Restrict)
        );
    }
}
