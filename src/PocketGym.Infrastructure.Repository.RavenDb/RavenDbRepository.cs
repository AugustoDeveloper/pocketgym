using PocketGym.Domain.Core.Entities;
using PocketGym.Domain.Repositories;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PocketGym.Infrastructure.Repository.RavenDb
{
    public abstract class RavenDbRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private bool disposed;

        protected IAsyncDocumentSession Session { get; }

        protected RavenDbRepository(IAsyncDocumentSession session)
        {
            Session = session;
        }
    
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await Session.StoreAsync(entity);
            await Session.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
        {
            var registeredEntities = await LoadByAsync(expression);
            foreach (var item in registeredEntities)
            {
                Session.Delete(item);
            }

            return true;
        }

        public Task<bool> ExistsByAsync(Expression<Func<TEntity, bool>> expression)
            => Session.Query<TEntity>().AnyAsync(expression);

        public string GenerateId()
            => Guid.NewGuid().ToString();

        public Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> expression)
            => Session.Query<TEntity>().FirstOrDefaultAsync(expression);

        public async Task<IEnumerable<TEntity>> LoadAllAsync()
            => await Session
                .Query<TEntity>()
                .ToListAsync();

        public async Task<IEnumerable<TEntity>> LoadByAsync(Expression<Func<TEntity, bool>> expression)
            => await Session
                .Query<TEntity>()
                .Where(expression, true)
                .ToListAsync();

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await Session.SaveChangesAsync();
            return entity;
        }

        protected abstract void InternalUpdate(TEntity newEntity, TEntity registered);

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !disposed)
            {
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void TestConnection()
        {
            Session.Query<TEntity>().FirstOrDefaultAsync().GetAwaiter().GetResult();
        }
    }
}
