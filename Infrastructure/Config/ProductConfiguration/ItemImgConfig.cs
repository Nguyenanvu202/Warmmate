using System;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config.ProductConfiguration;

public class ItemImgConfig : IEntityTypeConfiguration<ProductItemImg>
{
    public void Configure(EntityTypeBuilder<ProductItemImg> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.ImageUrl);

        builder.Property(p => p.CreatedBy).HasMaxLength(255);
		builder.Property(p => p.CreatedDate);
		builder.Property(p => p.UpdatedBy).HasMaxLength(255);
		builder.Property(p => p.UpdatedDate); 

    }
}
