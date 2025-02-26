using System;
using Core.Entities;
using Core.IRepository.ProductRelateRepo;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository;

public class OptionRepo(StoreContext storeContext) : IOptionRepo
{
    private readonly StoreContext _storeContext = storeContext;

    public void AddOption(VariationOpt option)
    {
        _storeContext.VariationOpts.Add(option);
    }

    public void DeleteOption(VariationOpt option)
    {
        _storeContext.VariationOpts.Remove(option);
    }

    public async Task<VariationOpt?> GetOptionById(int Id)
    {
        return await _storeContext.VariationOpts.FirstOrDefaultAsync(i => i.Id == Id);
    }

    public async Task<IReadOnlyList<VariationOpt>> GetOptionsAsync()
    {
        return await _storeContext.VariationOpts.ToListAsync();
    }

    public bool OptionExist(int Id)
    {
        return _storeContext.VariationOpts.Any(i => i.Id == Id);
    }

    public async Task<bool> SaveChangeAsync()
    {
        return await _storeContext.SaveChangesAsync() > 0;
    }

    public void UpdateOption(VariationOpt option)
    {
         _storeContext.Entry(option).State = EntityState.Modified;
    }
}
