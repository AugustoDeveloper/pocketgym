using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LiteDB;
using PocketGym.Domain.Core.Entities;
using PocketGym.Domain.Repositories;

namespace PocketGym.Infrastructure.Repository.LiteDb
{
    public abstract class LiteDbRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly string connectionString;
        private bool disposed;

        protected LiteCollection<TEntity> Collection { get; private set; }

        protected LiteDbRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public virtual Task<TEntity> AddAsync(TEntity entity)
        {
            return ExecuteAsync(() =>
            {
                BsonValue value = Collection.Insert(entity);
                entity.Id = value.AsInt64;

                return entity;
            });
        }

        public virtual Task<bool> DeleteAsync(TEntity entity)
        {
            return ExecuteAsync(() =>
            {
                return Collection.Delete(entity.Id);
            });
        }

        public virtual Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> expression)
        {
            return ExecuteAsync(() => Collection.FindOne(expression));
        }

        public virtual Task<IEnumerable<TEntity>> LoadAllAsync()
        {
            return ExecuteAsync(() => Collection.FindAll());
        }

        public virtual Task<TEntity> UpdateAsync(TEntity entity)
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

        public virtual Task<bool> ExistsByAsync(Expression<Func<TEntity, bool>> expression)
        {
            return ExecuteAsync(() =>
            {
                return Collection.Exists(expression);
            });
        }

        public virtual Task<IEnumerable<TEntity>> LoadByAsync(Expression<Func<TEntity, bool>> expression)
        {
            return ExecuteAsync(() => Collection.Find(expression));
        }

        protected Task<TResult> ExecuteAsync<TResult>(Func<TResult> func)
        {
            TResult result = default(TResult);
            using (var database = new LiteDatabase(connectionString))
            {
                try
                {
                    Collection = database.GetCollection<TEntity>();
                    Collection.IncludeAll();
                    result = func();
                }
                catch (Exception ex)
                {
                }
            }

            return Task.FromResult(result);
        }

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
    }
}