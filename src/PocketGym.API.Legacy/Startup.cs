using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using PocketGym.API.Configurations;
using PocketGym.Application.Services;
using PocketGym.Domain.Repositories;
using PocketGym.Infrastructure.CrossCutting.Mappings;
using PocketGym.Infrastructure.Repository.LiteDb;
using PocketGym.Infrastructure.Repository.LiteDb.Mapping;

namespace PocketGym.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();

            SigninConfigurations signinConfigurations = new SigninConfigurations();
            services.AddSingleton(signinConfigurations);

            services.AddTransient<IUserApplicationService, UserApplicationService>();
            services.AddTransient<ISerieApplicationService, SerieApplicationService>();
            services.AddTransient<IExerciseApplicationService, ExerciseApplicationService>();
            services.AddTransient<IExerciseSerieApplicationService, ExerciseSerieApplicationService>();

            services.AddTransient<IUserRepository>((v) => new UserRepository(Configuration["ConnectionStrings:PocketGymConnection"]));
            services.AddTransient<ISerieRepository>((v) => new SerieRepository(Configuration["ConnectionStrings:PocketGymConnection"]));
            services.AddTransient<IExerciseRepository>((v) => new ExerciseRepository(Configuration["ConnectionStrings:PocketGymConnection"]));

            services.AddAutoMapper(typeof(MappingProfile));
            services.InitializeDatabase(Configuration["ConnectionStrings:PocketGymConnection"],
                appSettings.AdministratorPasswordHash,
                appSettings.AdministratorPasswordSalt,
                appSettings.UpdateAdministratorPassword);
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    };
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddAuthorization(x =>
            {
                x.AddPolicy(JwtBearerDefaults.AuthenticationScheme,
                    new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(beareroptions =>
            {
                beareroptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signinConfigurations.Key,
                    ValidIssuer = appSettings.JWT.Issuer,
                    ValidAudience = appSettings.JWT.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                beareroptions.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }

                        return Task.CompletedTask;
                    }
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}