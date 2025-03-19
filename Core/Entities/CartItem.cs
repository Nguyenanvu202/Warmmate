using System;

namespace Core.Entities;

public class CartItem
{
    public int ItemId { get; set; }
    public required string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public required string PictureUrl { get; set; }
    public required string Color { get; set; }
    public required string Size { get; set; }
}
