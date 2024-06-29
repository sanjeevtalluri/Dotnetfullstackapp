using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreData.Entities;

namespace CoreData.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
    }
}