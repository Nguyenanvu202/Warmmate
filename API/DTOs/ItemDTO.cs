using System;
using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace API.DTOs;

public class ItemDTO
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }
    [Range(1, double.MaxValue, ErrorMessage = "Quantity as least greater than 1")]
    public int Quantity { get; set; }
    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public ICollection<ImgDTO> ProductItemImgs { get; } = new List<ImgDTO>();

    //Many to many with VariationOpt
    [Required]
    public List<OptDTO> VariationOpts { get; } = [];
}
