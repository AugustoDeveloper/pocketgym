using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using PocketGym.Domain.Core.Entities;
using PocketGym.Domain.Repositories;

namespace PocketGym.Infrastructure.Repository.MongoDb.Extensions
{
    public static class MongoDbServiceExtension
    {
        public static void InitializeMongoDb(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            BsonClassMap.RegisterClassMap<User>(cm =>
            {
                cm.AutoMap();
                cm.IdMemberMap.SetSerializer(new StringSerializer(BsonType.ObjectId));
                cm.MapIdMember(u => u.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
                cm.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<Target>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(s => s.Id);
                cm.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<Serie>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(s => s.Id);
                cm.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<SingleExerciseStep>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(s => s.Id);
                cm.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<ConjugateExerciseStep>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(s => s.Id);
                cm.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<DropSetExerciseStep>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(s => s.Id);
                cm.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<Exercise>(cm =>
            {
                cm.AutoMap();
                cm.IdMemberMap.SetSerializer(new StringSerializer(BsonType.ObjectId));
                cm.MapIdMember(u => u.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
                cm.SetIgnoreExtraElements(true);
            });

            serviceCollection.AddScoped<IUserRepository>(v => new UserRepository(configuration["MongoDb:ConnectionString"], configuration["MongoDb:DatabaseName"], "users"));
            serviceCollection.AddScoped<IExerciseRepository>(v => new ExerciseRepository(configuration["MongoDb:ConnectionString"], configuration["MongoDb:DatabaseName"], "exercises"));
        }
    }
}