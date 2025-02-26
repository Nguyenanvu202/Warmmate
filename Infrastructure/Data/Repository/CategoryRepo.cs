using System;
using Core.Entities;
using Core.IRepository.ProductRelateRepo;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository;

public class CategoryRepo(StoreContext storeContext) : ICategoryRepo
{
    private readonly StoreContext _storeContext = storeContext;

    public void AddCategory(ProductCategory category)
    {
        _storeContext.ProductCategories.Add(category);
    }

    public bool CategoryExist(int Id)
    {
        return _storeContext.ProductCategories.Any(i => i.Id == Id);
    }

    public void DeleteCategory(ProductCategory category)
    {
        _storeContext.ProductCategories.Remove(category);
    }

    public async Task<IReadOnlyList<ProductCategory>> GetAllCategoriesAsync()
    {
        return await _storeContext.ProductCategories.ToListAsync();
    }

    public async Task<IReadOnlyList<ProductCategory>> GetCategoriesByParentIdAsync(int Id)
    {
        return await _storeContext.ProductCategories.Where(i => i.ParentProductCategoryId == Id).ToListAsync();
    }

    public async Task<ProductCategory?> GetCategory(int Id)
    {
        return await _storeContext.ProductCategories.FirstOrDefaultAsync(i => i.Id == Id);
    }

    public async Task<bool> SaveChangeAsync()
    {
        return await _storeContext.SaveChangesAsync() > 0;
    }

    public void UpdateCategory(ProductCategory category)
    {
        _storeContext.Entry(category).State = EntityState.Modified;
    }
}
