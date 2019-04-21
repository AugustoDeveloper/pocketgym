using PocketGym.Domain.Core.Entities;
using PocketGym.Domain.Repositories;

namespace PocketGym.Infrastructure.Repository.LiteDb
{
    public class ExerciseRepository : LiteDbRepository<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(string connectionString) : base(connectionString)
        {
        }
    }
}