using System;

namespace Core.Entities;

public class ProductItem : BaseEntity
{
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    //dependent
    public int? ProductId { get; set; }
    public Product? Product { get; set; }
    //Principal
    public ICollection<ProductItemImg> ProductItemImgs { get; } = new List<ProductItemImg>();

    //Many to many with VariationOpt
    public List<VariationOpt> VariationOpts { get; } = [];
}
