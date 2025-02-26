using System;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config.ProductConfiguration;

public class VariationConfig : IEntityTypeConfiguration<Variation>
{
    public void Configure(EntityTypeBuilder<Variation> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Name).HasMaxLength(50).IsRequired();

        builder.Property(p => p.CreatedBy).HasMaxLength(255);
		builder.Property(p => p.CreatedDate);
		builder.Property(p => p.UpdatedBy).HasMaxLength(255);
		builder.Property(p => p.UpdatedDate); 
        //Relation with ProductCategory
        builder
        .HasOne(i=>i.ProductCategory)
        .WithMany(i=>i.Variations)
        .HasForeignKey(i => i.ProductCategoryId);
    }
}
