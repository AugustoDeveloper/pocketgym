using PocketGym.Domain.Core.Entities;
using PocketGym.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketGym.Infrastructure.Repository.MongoDb
{
    public class UserRepository : MongoDbRepository<User>, IUserRepository
    {
        public UserRepository(string connectionString, string databaseName, string collectionName) : base(connectionString, databaseName, collectionName)
        {
        }
    }
}