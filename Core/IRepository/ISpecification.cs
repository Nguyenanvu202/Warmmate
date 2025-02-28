using System;
using System.Linq.Expressions;

namespace Core.IRepository;

public interface ISpecification<T> 
{
    Expression<Func<T,bool>>? Criteria {get;}
    bool IsDistinct {get;}

}
public interface ISpecification<T,TResult> : ISpecification<T>{
    Expression<Func<T, IEnumerable<TResult>>>? SelectMany {get;}
}
