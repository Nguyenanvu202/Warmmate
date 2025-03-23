using System;

namespace API.DTOs;

public class AddressDTO
{
    public required string Line1 { get; set; }
    public string? Line2 { get; set; }

    public required string City { get; set; }
    public required string Quan { get; set; }    
    public required string Huyen { get; set; }

}
