using System;
using System.Linq.Expressions;
using Core.IRepository;

namespace Core.Specification;

public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
{
    protected BaseSpecification(): this(null){}
    public Expression<Func<T, bool>>? Criteria => criteria;

    public bool IsDistinct {get; private set;}

    public int Take {get; private set;}
    public int Skip {get; private set;}
    public string? Search { get; private set; }

    public bool IsPagingEnabled {get; private set;}

    public IQueryable<T> ApplyCriteria(IQueryable<T> query)
    {
        if(Criteria != null){
            query = query.Where(Criteria);
        }
        return query;
    }

    protected void ApplyDistict(){
        IsDistinct = true;
    }
    protected void ApplyPagination(int skip, int take){
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
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
