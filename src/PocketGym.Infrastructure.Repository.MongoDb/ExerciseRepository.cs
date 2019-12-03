using PocketGym.Domain.Core.Entities;
using PocketGym.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketGym.Infrastructure.Repository.MongoDb
{
    public class ExerciseRepository : MongoDbRepository<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(string connectionString, string databaseName, string collectionName) : base(connectionString, databaseName, collectionName)
        {
        }
    }
}