using System;
using Core.Entities;

namespace Core.Specification;

public class ProductSpecification : BaseSpecification<ProductItem>
{
    public ProductSpecification(string[] options) : base(x => x.VariationOpts.Any(opt => options.Contains(opt.Value)))
    {
        
    }
}
