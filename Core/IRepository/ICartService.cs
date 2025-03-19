using System;
using Core.Entities;

namespace Core.IRepository;

public interface ICartService
{
    Task<ShoppingCart?> GetShoppingCartAsync(string key);
    Task<ShoppingCart?> SetShoppingCartAsync(ShoppingCart cart);
    Task<bool> DeleteCartAsync(string key);

}
