using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CoreData.Entities;
using CoreData.Specifications;

namespace CoreData.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetByIdAsyncWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAllAsyncWithSpec(ISpecification<T> spec);
    }
}