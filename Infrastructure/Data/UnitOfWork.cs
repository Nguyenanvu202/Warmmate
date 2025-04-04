using System;
using System.Collections.Concurrent;
using Core.Entities;
using Core.IRepository;

namespace Infrastructure.Data.Repository;

public class UnitOfWork(StoreContext _storeContext) : IUnitOfWork
{
    private readonly ConcurrentDictionary<string, object> _repositories = new();
    public async Task<bool> Complete()
    {
        return await _storeContext.SaveChangesAsync() > 0;
    }

    public IGenericRepo<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        var type  = typeof(TEntity).Name;
        return(IGenericRepo<TEntity>)_repositories.GetOrAdd(type, t =>{
             var repositoryType = typeof(GenericRepo<>).MakeGenericType(typeof(TEntity));
            return Activator.CreateInstance(repositoryType, _storeContext)
                ?? throw new InvalidOperationException(
                    $"Could not create repository instance for {t}");
        });
    }
}
