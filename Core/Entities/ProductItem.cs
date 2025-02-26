using System;

namespace Core.Entities;

public class ProductItem : BaseEntity
{
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; } = "";
    //dependent
    public int? ProductCategoryId { get; set; }
    public ProductCategory? ProductCategory { get; set; }
    //Principal
    public ICollection<ProductItemImg> ProductItemImgs { get; } = new List<ProductItemImg>();

    //Many to many with VariationOpt
    public List<VariationOpt> VariationOpts { get; } = [];
}
