using System;

namespace Core.Entities.OrderAggregate;

public class ShippingAddress
{
    public required string Name { get; set; }
    public required string Line1 { get; set; }
    public string? Line2 { get; set; }
    public required string City { get; set; }
    public required string Huyen { get; set; }
    public required string Quan { get; set; }
}
