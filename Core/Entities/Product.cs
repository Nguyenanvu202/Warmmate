using System;

namespace Core.Entities;

public class Product : BaseEntity
{
    public string ImageUrl { get; set; } = "";
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";

    //Principal
    public ICollection<ProductItem> ProductItems { get; } = new List<ProductItem>();

    //Dependent
    public int? ProductCategoryId { get; set; }
    public ProductCategory? ProductCategory { get; set; }

}
