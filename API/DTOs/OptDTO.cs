using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class OptDTO
{
    [Required]
    public string Value { get; set; } = string.Empty;

}
