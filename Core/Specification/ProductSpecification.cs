using System;
using Core.Entities;

namespace Core.Specification;

public class ProductSpecification : BaseSpecification<ProductItem, VariationOpt>
{
    public ProductSpecification(ProductSpecificationParams specParams) : base(x =>
    (string.IsNullOrEmpty(specParams.Search) || x.Name!.ToLower().Contains(specParams.Search)) &&
    (specParams.Options.Count == 0) || x.VariationOpts.
    Any(opt => specParams.Options.Contains(opt.Value)))
    {
        ApplyPagination(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        AddInclude(p => p.ProductItemImgs);
        AddInclude(p => p.VariationOpts);
    }

    public ProductSpecification(ProductSpecificationParams specParams, int categoryId) : base(x =>
    x.ProductCategory!.ParentProductCategoryId == categoryId &&
    (string.IsNullOrEmpty(specParams.Search) ||
    x.Name!.ToLower().Contains(specParams.Search)) && (specParams.Options.Count == 0) ||
    x.VariationOpts.Any(opt => specParams.Options.Contains(opt.Value)))
    {
        ApplyPagination(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        AddInclude(p => p.ProductItemImgs);
        AddInclude(p => p.VariationOpts);
    }

    public ProductSpecification(int Id) : base(x => x.Id == Id)
    {
        AddInclude(p => p.ProductItemImgs);
        AddInclude(p => p.VariationOpts);
    }

    public ProductSpecification(int productId, int optionId) : base(x => x.Id == productId)
    {
        AddSelectMany(x => x.VariationOpts.Where(x => x.VariationId==optionId));
        ApplyDistict();
    }

}
