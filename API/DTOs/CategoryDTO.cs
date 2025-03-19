using System;

namespace API.DTOs;

public class CategoryDTO
{
    public string? name { get; set; }
    public CategoryDTO? ParentCategoryId { get; set; }
}
