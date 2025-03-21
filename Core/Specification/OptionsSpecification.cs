using System;
using Core.Entities;

namespace Core.Specification;

public class OptionsSpecification : BaseSpecification<ProductItem, VariationOpt>
{

  public OptionsSpecification(string name)
  {
    AddSelectMany(x => x.VariationOpts.Where(x => x.Variation != null && x.Variation.Name != null && x.Variation.Name.ToLower() == name.ToLower()));
    ApplyDistict();
  }


}
