using System;
using Core.Entities;

namespace Core.IRepository.ProductRelateRepo;

public interface IOptionRepo
{
    Task<IReadOnlyList<VariationOpt>> GetOptionsAsync();
    Task<VariationOpt?> GetOptionById( int Id);
    void AddOption( VariationOpt option);
    void UpdateOption(VariationOpt option );
    void DeleteOption(VariationOpt option);
    bool OptionExist(int Id);
    Task<bool> SaveChangeAsync();
}
