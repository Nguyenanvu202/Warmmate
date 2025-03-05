using System;
using Core.Entities;

namespace Core.Specification;

public class ProductSpecification : BaseSpecification<ProductItem>
{
    public ProductSpecification(ProductSpecificationParams specParams) : base(x => 
    (string.IsNullOrEmpty(specParams.Search)||x.Name.ToLower().Contains(specParams.Search))&&
    (specParams.Options.Count == 0) ||x.VariationOpts.
    Any(opt => specParams.Options.Contains(opt.Value)))
    {
        ApplyPagination(specParams.PageSize *(specParams.PageIndex -1), specParams.PageSize);
    }

}
