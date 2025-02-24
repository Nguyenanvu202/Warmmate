using System;

namespace Core.Entities;

public class Variation : BaseEntity
{
    public required string Name { get; set; }

    //dependent
    public ProductCategory? ProductCategory { get; set; }
    public int? ProductCategoryId { get; set; }
    //Principal
    public ICollection<VariationOpt> VariationOpts { get; } = new List<VariationOpt>();
}
