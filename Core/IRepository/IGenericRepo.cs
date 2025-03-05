using System;
using Core.Entities;

namespace Core.IRepository;

public interface IGenericRepo<T> where T : BaseEntity
{   
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int Id);
    //Spec
    Task<IReadOnlyList<T>> GetAllBySpec(ISpecification<T> spec);
    Task<T?> GetBySpec(ISpecification<T> spec);
    Task<IReadOnlyList<TResult>> GetAllBySpec<TResult>(ISpecification<T,TResult> spec);
    // Task<TResult?> GetBySpec<TResult>(ISpecification<T,TResult> spec);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    bool Exist(int Id);
    Task<bool> SaveChangeAsync();
    Task<int> CountAsync(ISpecification<T> spec);
}
