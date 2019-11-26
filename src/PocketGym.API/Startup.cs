using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PocketGym.API.Auth;
using PocketGym.Application.Services;
using PocketGym.Domain.Repositories;
using PocketGym.Infrastructure.CrossCutting.Mappings;
using PocketGym.Infrastructure.Repository.MongoDb;
using PocketGym.Infrastructure.Repository.MongoDb.Extensions;

namespace PocketGym.API
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables("PocketGym_");
            Configuration = builder.Build();
            Configuration.AsEnumerable().ToList().ForEach(c => Console.WriteLine($"{c.Key}=>{c.Value}"));
            Console.WriteLine("->"+Configuration["Auth:Secret"]);
            Console.WriteLine("->"+Configuration["MongoDb:ConnectionString"]);
            Console.WriteLine("->"+Configuration["MongoDb:DatabaseName"]);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<IUserApplicationService, UserApplicationService>();
            services.AddTransient<ISerieApplicationService, SerieApplicationService>();
            services.AddTransient<IExerciseApplicationService, ExerciseApplicationService>();
            
            services.AddAutoMapper(typeof(MappingProfile));
            services.InitializeMongoDb(Configuration);
            services.AddBearerTokenValidation(Configuration["Auth_Secret"]);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
