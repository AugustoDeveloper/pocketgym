using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PocketGym.Domain.Core.Entities;

namespace PocketGym.Domain.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class, IEntity
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> LoadByAsync(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> LoadAllAsync();
        Task<bool> DeleteAsync(TEntity entity);
        Task<bool> ExistsByAsync(Expression<Func<TEntity, bool>> expression);
    }
}