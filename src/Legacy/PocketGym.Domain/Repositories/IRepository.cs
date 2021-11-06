using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PocketGym.Domain.Core.Entities;

namespace PocketGym.Domain.Repositories
{
    public interface IRepository : IDisposable 
    {
        void TestConnection();
     }
    public interface IRepository<TEntity> : IRepository where TEntity : class, IEntity
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> LoadByAsync(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> LoadAllAsync();
        Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression);
        Task<bool> ExistsByAsync(Expression<Func<TEntity, bool>> expression);
        string GenerateId();
    }
}