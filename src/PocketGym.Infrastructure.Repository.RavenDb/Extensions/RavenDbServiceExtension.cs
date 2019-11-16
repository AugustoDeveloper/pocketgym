using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PocketGym.Domain.Repositories;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PocketGym.Infrastructure.Repository.RavenDb.Extensions
{
    public static class RavenDbServiceExtension
    {
        public static void InitializeRavenDb(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var databaseName = configuration["RavenDb:PocketGymDb:DatabaseName"];
            var urls = configuration["RavenDb:PocketGymDb:Url"];
            var certPath = configuration["RavenDb:PocketGymDb:CertPath"];
            var certPass = configuration["RavenDb:PocketGymDb:CertPass"];

            var store = new DocumentStore
            {
                Urls = new[] { urls },
                Database = databaseName,
                Certificate = new X509Certificate2(certPath, certPass),
            };
            store.Conventions.IdentityPartsSeparator = "-";
            store.Initialize();
            serviceCollection.AddSingleton<IDocumentStore>(store);
            serviceCollection.AddTransient<IUserRepository, UserRepository>();

            serviceCollection.AddScoped(serviceProvider =>
            {
                return serviceProvider
                    .GetService<IDocumentStore>()
                    .OpenAsyncSession();
            });
        }
    }
}
