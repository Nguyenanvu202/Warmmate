using System;

namespace Core.Entities;

public class VariationOpt : BaseEntity
{
    public string Value { get; set; } = "";

    //Many to Many with ProductItem
    public List<ProductItem> ProductItems { get; } = [];
    //denpendent
    public int? VariationId { get; set; }
    public Variation? Variation { get; set; }
}
