using System;
using Core.Entities;

namespace Core.IRepository.ProductRelateRepo;

public interface ICategoryRepo
{
    //Categories
    Task<IReadOnlyList<ProductCategory>> GetAllCategoriesAsync();
    Task<IReadOnlyList<ProductCategory>> GetCategoriesByParentIdAsync(int ParentId);
    Task<ProductCategory?> GetCategory(int Id);
    void AddCategory( ProductCategory category);
    void UpdateCategory(ProductCategory category );
    void DeleteCategory(ProductCategory category);
    bool CategoryExist(int Id);
    Task<bool> SaveChangeAsync();
    

}
