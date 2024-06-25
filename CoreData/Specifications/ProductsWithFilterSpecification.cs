using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreData.Entities;

namespace CoreData.Specifications
{
    public class ProductsWithFilterSpecification : BaseSpecification<Product>
    {
        public ProductsWithFilterSpecification(ProductSpecParams productParams)
        : base(x =>
        (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
        (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
        (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
        )
        {

        }

    }
}