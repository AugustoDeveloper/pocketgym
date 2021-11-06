using System;
using LiteDB;
using Microsoft.Extensions.DependencyInjection;
using PocketGym.Domain.Core.Entities;

namespace PocketGym.Infrastructure.Repository.LiteDb.Mapping
{
    public static class EntityMapping
    {
        public static void InitializeDatabase(this IServiceCollection collection, 
            string connectionString, 
            string passwordHash, 
            string passwordSalt, 
            bool update)
        {
            collection.MapRepo();
            try
            {
                using (var database = new LiteDatabase(connectionString))
                {
                    CreateAdminUser(database.GetCollection<User>(), new User
                    {
                        Name = "Administrator",
                        Username = "admin",
                        PasswordHash = Convert.FromBase64String(passwordHash),
                        PasswordSalt = Convert.FromBase64String(passwordSalt),
                        Age = Int16.MaxValue,
                        Role = new Role { RoleName = "Admin" }
                    }, update);
                }
            }
            catch
            { }
        }

        private static void CreateAdminUser(LiteCollection<User> collection, User user, bool update)
        {
            if (!collection.Exists(u => u.Username == user.Username))
            {
                collection.Insert(user);
            }
            else
            {
                if (update)
                {
                    var oldUser = collection.FindOne(u => u.Username == user.Username);
                    user.Id = oldUser.Id;
                    collection.Update(user);
                }
            }
        }

        public static void MapRepo(this IServiceCollection collection)
        {
            var mapper = BsonMapper.Global;
            mapper.Entity<Exercise>()
                .Id(e => e.Id, autoId: true)
                .Field(e => e.Description, "description")
                .Field(e => e.Name, "name")
                .Field(e => e.MuscleGroup, "muscle_group");

            mapper.Entity<Serie>()
                .Id(s => s.Id, autoId: true)
                .Field(s => s.Name, "name")
                .Field(s => s.UserId, "user_id")
                .Field(s => s.ExercisesSeries, "exercises_serie");

            mapper.Entity<User>()
                .Id(u => u.Id, autoId: true)
                .Field(u => u.Name, "name")
                .Field(u => u.Age, "age")
                .Field(u => u.Role, "role")
                .Field(u => u.PasswordSalt, "password_salt")
                .Field(u => u.PasswordHash, "password_hash");
        }
    }
}