using System;
using System.Threading.Tasks;
using Core.Entities;
using Core.IRepository.ProductRelateRepo;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Infrastructure.Data.Repository;

public class ItemRepo(StoreContext storeContext) : IItemRepo
{
    private readonly StoreContext _storeContext = storeContext;

    public void AddItem(ProductItem item)
    {
        _storeContext.ProductItems.Add(item);
    }

    public void DeleteItem(ProductItem item)
    {
        _storeContext.ProductItems.Add(item);
    }

    public async Task<ProductItem?> GetItemById(int Id)
    {
        return await _storeContext.ProductItems.FirstOrDefaultAsync(i => i.Id == Id);
    }

    // public async Task<IReadOnlyList<ProductItem>> GetItemsAsync()
    // {
    //     return await _storeContext.ProductItems.ToListAsync();
    // }


    public async Task<ProductItem> GetItemsAsync(int id)
    {        
        return await _storeContext.ProductItems
        .Include(x => x.ProductItemImgs) // Include ProductItemImgs
        .Include(x => x.VariationOpts)   // Include VariationOpts
        .FirstOrDefaultAsync(x=>x.Id == id);
    }

    public async Task<IReadOnlyList<ProductItem>> GetItemsByCategoryId(int CategoryId)
    {
        return await _storeContext.ProductItems.Where(x => x.ProductCategoryId == CategoryId).ToListAsync();
    }

    public async Task<IReadOnlyList<ProductItem>> GetItemsByColorId(int ColorId)
    {
        return await _storeContext.ProductItems.Where(x => x.VariationOpts.Any(x => x.Id == ColorId)).ToListAsync();
    }

    public async Task<IReadOnlyList<VariationOpt>> GetOptionsByVariation(string? name)
    {
        if (string.IsNullOrEmpty(name))
        {
            // Return an empty list or throw an exception if the name is null or empty
            return new List<VariationOpt>();
        }

        return await _storeContext.VariationOpts
            .Where(x => x.Variation != null && x.Variation.Name != null && x.Variation.Name.ToLower() == name.ToLower())
            .ToListAsync();
    }

    // public  async Task<IReadOnlyList<VariationOpt>> GetOptionsByVariationTest(string? name)
    // {

    //     return await _storeContext.ProductItems.
    // }

    public bool ItemExist(int Id)
    {
        return _storeContext.ProductItems.Any(i => i.Id == Id);
    }

    public async Task<bool> SaveChangeAsync()
    {
        return await _storeContext.SaveChangesAsync() > 0;
    }

    public void UpdateItem(ProductItem item)
    {
        _storeContext.Entry(item).State = EntityState.Modified;
    }
}
