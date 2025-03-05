using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class ImgDTO
{
    [Required]
    public string ImageUrl { get; set; } = string.Empty;

}
