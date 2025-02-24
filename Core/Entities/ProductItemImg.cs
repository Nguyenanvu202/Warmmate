using System;

namespace Core.Entities;

public class ProductItemImg : BaseEntity
{
    public string ImageUrl { get; set; } = "";

    //dependent
    public int? ProductItemId { get; set; }
    public ProductItem? ProductItem { get; set; }
}
