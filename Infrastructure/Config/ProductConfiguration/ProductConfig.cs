using System;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config.ProductConfig;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey (x => x.Id);
        builder.Property(n => n.Name).IsRequired().HasMaxLength(255);
        builder.Property(n => n.ImageUrl).IsRequired();
        builder.Property(n => n.Description).HasMaxLength(500);

        builder.Property(p => p.CreatedBy).HasMaxLength(255);
		builder.Property(p => p.CreatedDate);
		builder.Property(p => p.UpdatedBy).HasMaxLength(255);
		builder.Property(p => p.UpdatedDate); 

        //Relation with ProductCategory
        builder
        .HasOne(p => p.ProductCategory)
        .WithMany(p => p.Products)
        .HasForeignKey(i => i.ProductCategoryId);
    }
}
