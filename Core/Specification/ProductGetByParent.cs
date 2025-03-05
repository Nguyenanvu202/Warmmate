using System;
using Core.Entities;

namespace Core.Specification;

public class ProductGetByParent : BaseSpecification<ProductItem>
{
    public ProductGetByParent(int Id) : base (x => x.ProductCategory.Id == Id)
    {
        //ao => ao thun => detail
    }
    
}
