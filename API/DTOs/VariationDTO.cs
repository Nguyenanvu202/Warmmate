using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class VariationDTO
{
    [Required]
    public string name { get; set; } = string.Empty;
}
