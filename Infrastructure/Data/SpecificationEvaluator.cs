using System;
using System.Collections;
using Core.Entities;
using Core.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
    {
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }
        if (spec.IsPagingEnabled)
        {
            query = query.Skip(spec.Skip).Take(spec.Take);
        }

        return query;


        // if(spec.IsDistinct){
        //     query = query.Distinct();
        // }
    }

    public static IQueryable<TResult> GetQuery<TSpec, TResult>(IQueryable<T> query, ISpecification<T, TResult> spec)
    {
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }
        var selectQuery = query as IQueryable<TResult>;
        if (spec.SelectMany != null)
        {
            selectQuery = query.SelectMany(spec.SelectMany);
        }
        if (spec.IsDistinct)
        {
            selectQuery = selectQuery?.Distinct();
        }
        if (spec.IsPagingEnabled)
        {
            selectQuery = selectQuery?.Skip(spec.Skip).Take(spec.Take);
        }
        return selectQuery ?? query.Cast<TResult>();
    }
}


