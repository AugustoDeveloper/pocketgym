using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using PocketGym.Domain.Core.Entities;
using PocketGym.Domain.Repositories;

namespace PocketGym.Infrastructure.Repository.LiteDb
{
    public abstract class LiteDbRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private Lazy<LiteDatabase> lazyLiteDatabse;
        private bool disposed;

        private LiteDatabase Database => lazyLiteDatabse.Value;
        protected LiteCollection<TEntity> Collection => Database.GetCollection<TEntity>();

        protected LiteDbRepository(string connectionString)
        {
            lazyLiteDatabse = new Lazy<LiteDatabase>(() => new LiteDatabase(connectionString));
        }

        public Task<TEntity> AddAsync(TEntity entity)
        {
            return ExecuteAsync(() =>
            {
                BsonValue value = Collection.Insert(entity);
                entity.Id = value.AsInt64;

                return entity;
            });
        }

        public Task<bool> DeleteAsync(TEntity entity)
        {
            return ExecuteAsync(() =>
            {
                return Collection.Delete(entity.Id);
            });
        }

        public Task<TEntity> GetByIdAsync(long id)
        {
            return ExecuteAsync(() => Collection.FindById(id));
        }

        public Task<IEnumerable<TEntity>> LoadAllAsync()
        {
            return ExecuteAsync(() => Collection.FindAll());
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            return ExecuteAsync(() =>
            {
                if (Collection.Update(entity) == false)
                {
                    return null;
                }

                return entity;
            });
        }

        public Task<bool> ExistsByAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> expression)
        {
            return ExecuteAsync(() =>
            {
                return Collection.Exists(expression);
            });
        }

        protected Task<TResult> ExecuteAsync<TResult>(Func<TResult> func)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    var result = func();
                    return result;
                }
                catch(Exception ex)
                {
                    return default(TResult);
                }
            });
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !disposed)
            {
                Database.Dispose();
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}