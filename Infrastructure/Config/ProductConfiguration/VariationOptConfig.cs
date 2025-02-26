using System;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config.ProductConfiguration;

public class VariationOptConfig : IEntityTypeConfiguration<VariationOpt>
{
    public void Configure(EntityTypeBuilder<VariationOpt> builder)
    {
        builder.HasKey(i=>i.Id);
        builder.Property(i=> i.Value).HasMaxLength(50).IsRequired();

        builder.Property(p => p.CreatedBy).HasMaxLength(255);
		builder.Property(p => p.CreatedDate);
		builder.Property(p => p.UpdatedBy).HasMaxLength(255);
		builder.Property(p => p.UpdatedDate);   
        //Relation with Variation
        builder
        .HasOne(p => p.Variation)
        .WithMany(p=>p.VariationOpts)
        .HasForeignKey(p => p.VariationId);
    }
}
