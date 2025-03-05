using System;
using System.Collections;
using System.Threading.Tasks;
using Core.Entities;
using Core.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository;

public class GenericRepo<T>(StoreContext _storeContext) : IGenericRepo<T> where T : BaseEntity
{
    public void Add(T entity)
    {
        _storeContext.Set<T>().Add(entity);
    }

    public void Delete(T entity)
    {
        _storeContext.Set<T>().Remove(entity);
    }

    public bool Exist(int Id)
    {
       return _storeContext.Set<T>().Any(i => i.Id == Id);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
       return await _storeContext.Set<T>().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllBySpec(ISpecification<T> spec)
    {
        var query = ApplySpec(spec);
        return await query.ToListAsync();
    }

        public async Task<T?> GetBySpec(ISpecification<T> spec)
    {
        var query = ApplySpec(spec);
        return await query.FirstOrDefaultAsync();
    }  

    public async Task<T?> GetByIdAsync(int Id)
    {
        return await _storeContext.Set<T>().FirstOrDefaultAsync(i => i.Id == Id);
    }



    public async Task<bool> SaveChangeAsync()
    {
        return await _storeContext.SaveChangesAsync() > 0;
    }

    public void Update(T entity)
    {
       _storeContext.Set<T>().Attach(entity);
       _storeContext.Entry(entity).State = EntityState.Modified;
    }
    public async Task<IReadOnlyList<TResult>> GetAllBySpec<TResult>(ISpecification<T, TResult> spec)
    {
        return await ApplySpec(spec).ToListAsync();
    }

    public async Task<TResult?> GetBySpec<TResult>(ISpecification<T, TResult> spec)
    {
        return await ApplySpec(spec).FirstOrDefaultAsync();
    }
    private IQueryable<T> ApplySpec(ISpecification<T> spec){
        return SpecificationEvaluator<T>.GetQuery(_storeContext.Set<T>().AsQueryable(),spec);
    }

    private IQueryable<TResult> ApplySpec<TResult>(ISpecification<T, TResult> spec){

        return SpecificationEvaluator<T>.GetQuery<T,TResult>(_storeContext.Set<T>().AsQueryable(),spec);
    }

    public async Task<int> CountAsync(ISpecification<T> spec)
    {
        var query = _storeContext.Set<T>().AsQueryable();
        query = spec.ApplyCriteria(query);
        return await query.CountAsync();
    }
}

