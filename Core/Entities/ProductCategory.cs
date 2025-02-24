using System;
using System.Runtime.CompilerServices;

namespace Core.Entities;

public class ProductCategory : BaseEntity
{
    public required string Name { get; set; } 

    //Self-Referential Relationship
    public int? ParentProductCategoryId { get; set; }
    public ProductCategory? ParentProductCategory { get; set; }
    public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

    //Principal
    public ICollection<Product> Products { get; } = new List<Product>();
    public ICollection<Variation> Variations { get; } = new List<Variation>();
}
