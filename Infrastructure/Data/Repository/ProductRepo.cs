using System;
using Core.Entities;
using Core.IRepository.ProducrRelateRepo;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository;

public class ProductRepo(StoreContext storeContext) : IProductRepo
{
    private readonly StoreContext _storeContext = storeContext;

    public void AddProduct(Product product)
    {
        _storeContext.Products.Add(product);
    }

    public void DeleteProduct( Product product)
    {
        
        _storeContext.Products.Remove(product);
        
    }

    public async Task<Product?> GetProductById(int Id)
    {
        return await _storeContext.Products
        .FirstOrDefaultAsync(p => p.Id == Id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return await _storeContext.Products.ToListAsync();
    }

    public bool ProductExist(int Id)
    {
        return _storeContext.Products.Any(i => i.Id == Id);
    }

    public async Task<bool> SaveChangeAsync()
    {
        return await _storeContext.SaveChangesAsync() > 0;
    }

    public void UpdateProduct(Product product)
    {
        _storeContext.Entry(product).State = EntityState.Modified;
    }
}
