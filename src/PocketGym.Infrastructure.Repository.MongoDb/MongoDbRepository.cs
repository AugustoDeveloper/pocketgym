using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using PocketGym.Domain.Core.Entities;
using PocketGym.Domain.Repositories;

namespace PocketGym.Infrastructure.Repository.MongoDb
{
    public abstract class MongoDbRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly string connectionString;
        private readonly string collectionName;
        private bool disposed;
        private Lazy<MongoClient> lazyClient;
        private Lazy<IMongoDatabase> lazyDatabase;
        
        protected IMongoCollection<TEntity> Collection => lazyDatabase.Value.GetCollection<TEntity>(collectionName);
        
        protected MongoDbRepository(string connectionString, string databaseName, string collectionName)
        {
            this.connectionString = connectionString;
            this.collectionName = collectionName;
            lazyClient = new Lazy<MongoClient>(() => new MongoClient(this.connectionString));
            lazyDatabase = new Lazy<IMongoDatabase>(() => lazyClient.Value.GetDatabase(databaseName));
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await Collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var replaced = await Collection.ReplaceOneAsync(e => e.Id == entity.Id, entity);

            return entity;
        }

        public async Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> expression) 
            => await Collection.FindSync(expression).FirstOrDefaultAsync();

        public async Task<IEnumerable<TEntity>> LoadByAsync(Expression<Func<TEntity, bool>> expression)
        {
            var searchedList = await Collection.FindAsync(expression);

            return searchedList.ToList();
        }

        public async Task<IEnumerable<TEntity>> LoadAllAsync()
        {
            var allResults = await Collection.FindAsync(e => true);
            return allResults.ToList();
        }

        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
        {
            var deletedResult = await Collection.DeleteManyAsync(expression);
            return deletedResult.DeletedCount > 0;
        }

        public async Task<bool> ExistsByAsync(Expression<Func<TEntity, bool>> expression)
            => await GetByAsync(expression) != null;

        public string GenerateId() => ObjectId.GenerateNewId().ToString();
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