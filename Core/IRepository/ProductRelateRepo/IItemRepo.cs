using System;
using Core.Entities;

namespace Core.IRepository.ProductRelateRepo;

public interface IItemRepo
{
    Task<IReadOnlyList<ProductItem>> GetItemsAsync(int[] optionsId);
    Task<IReadOnlyList<ProductItem>> GetItemsByCategoryId(int categoryId);
    Task<IReadOnlyList<VariationOpt>> GetOptionsByVariation(string? name);
    Task<ProductItem?> GetItemById( int Id);
    void AddItem( ProductItem item);
    void UpdateItem(ProductItem item );
    void DeleteItem(ProductItem item);
    bool ItemExist(int Id);
    Task<bool> SaveChangeAsync();
}
