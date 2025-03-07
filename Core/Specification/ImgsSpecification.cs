using System;
using Core.Entities;

namespace Core.Specification;

public class ImgsSpecification :BaseSpecification<ProductItem, ProductItemImg>
{
  public ImgsSpecification(int Id)
  {
        AddSelectMany(x => x.ProductItemImgs.Where(x => x.ProductItemId==Id));
        ApplyDistict();
  }
}
