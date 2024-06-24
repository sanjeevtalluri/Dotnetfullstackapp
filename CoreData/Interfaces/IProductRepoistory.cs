using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreData.Entities;

namespace CoreData.Interfaces
{
    public interface IProductRepoistory
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
    }
}