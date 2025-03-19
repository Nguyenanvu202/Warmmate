using System;
using Core.Entities;

namespace Core.Specification;

public class ProductGetByParent : BaseSpecification<ProductItem>
{
    public ProductGetByParent(ProductSpecificationParams specParams, int Id) : base (x => x.ProductCategory.ParentProductCategoryId == Id)
    {
        ApplyPagination(specParams.PageSize *(specParams.PageIndex -1), specParams.PageSize);
        AddInclude(p => p.ProductItemImgs);
        AddInclude(p => p.VariationOpts);
    }
    
}
