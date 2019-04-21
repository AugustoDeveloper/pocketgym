using PocketGym.Domain.Core.Entities;
using PocketGym.Domain.Repositories;

namespace PocketGym.Infrastructure.Repository.LiteDb
{
    public class SerieRepository : LiteDbRepository<Serie>, ISerieRepository
    {
        public SerieRepository(string connectionString) : base(connectionString)
        {
        }
    }
}