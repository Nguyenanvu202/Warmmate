using System;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config.ProductConfig;

public class ProductCategoryConfig : IEntityTypeConfiguration<Core.Entities.ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.HasKey (x => x.Id);
        builder.Property(n => n.Name).IsRequired().HasMaxLength(255);
        

        builder.Property(p => p.CreatedBy).HasMaxLength(255);
		builder.Property(p => p.CreatedDate);
		builder.Property(p => p.UpdatedBy).HasMaxLength(255);
		builder.Property(p => p.UpdatedDate); 

        //Self Relation
        builder
            .HasMany(e => e.ProductCategories)
            .WithOne(e => e.ParentProductCategory)
            .HasForeignKey(i => i.ParentProductCategoryId);
            
    }
}
