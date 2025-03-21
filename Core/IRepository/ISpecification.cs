using System;
using System.Linq.Expressions;

namespace Core.IRepository;

public interface ISpecification<T> 
{
    Expression<Func<T,bool>>? Criteria {get;}
    bool IsDistinct {get;}
    int Take {get;}
    int Skip {get;}
    bool IsPagingEnabled {get;}
    public List<Expression<Func<T, object>>> Includes { get; }
    IQueryable<T> ApplyCriteria(IQueryable<T> query);

}
public interface ISpecification<T,TResult> : ISpecification<T>{
    Expression<Func<T, IEnumerable<TResult>>>? SelectMany {get;}
    Expression<Func<T, TResult>>? Select {get;}
    
}
