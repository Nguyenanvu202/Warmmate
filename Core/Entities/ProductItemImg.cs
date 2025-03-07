using System;
using System.Text.Json.Serialization;

namespace Core.Entities;

public class ProductItemImg : BaseEntity
{
    public string ImageUrl { get; set; } = "";

    //dependent
    [JsonIgnore]
    public int? ProductItemId { get; set; }
    public ProductItem? ProductItem { get; set; }
}
