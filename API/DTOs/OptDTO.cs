using System;
using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace API.DTOs;

public class OptDTO
{
    [Required]
    public int? VariationId { get; set; }

    [Required]
    public string Value { get; set; } = string.Empty;



}
