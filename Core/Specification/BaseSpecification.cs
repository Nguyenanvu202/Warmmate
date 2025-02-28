using System;
using System.Linq.Expressions;
using Core.IRepository;

namespace Core.Specification;

public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
{
    protected BaseSpecification(): this(null){}
    public Expression<Func<T, bool>>? Criteria => criteria;

    public bool IsDistinct {get; private set;}
    protected void ApplyDistict(){
        IsDistinct = true;
    }
}

public class BaseSpecification<T, TResult>(Expression<Func<T, bool>>? criteria) : BaseSpecification<T>(criteria), ISpecification<T,TResult>
{
    protected BaseSpecification(): this(null!){}
    // Add a property for the selector (projection)
    public Expression<Func<T, TResult>>? Select {get; private set;}

    public Expression<Func<T, IEnumerable<TResult>>>? SelectMany {get; private set;}

    protected void AddSelectMany(Expression<Func<T, IEnumerable<TResult>>> selectExpression){
        SelectMany = selectExpression;
        
    }
    

}
