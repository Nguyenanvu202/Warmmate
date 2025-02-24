using System;
using Core.Entities;

namespace Core.IRepository.ProducrRelateRepo;

public interface IProductRepo
{
    Task<IReadOnlyList<Product>> GetProductsAsync();
    Task<Product?> GetProductById( int Id);
    void AddProduct( Product product);
    void UpdateProduct(Product product );
    void DeleteProduct(Product product);
    bool ProductExist(int Id);
    Task<bool> SaveChangeAsync();
}
