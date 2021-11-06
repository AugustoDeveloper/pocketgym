using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using PocketGym.Application.Core.Dtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace PocketGym.API.Auth
{
    public static class Tokenrization
    {
        public static string Secret { get; private set; }
        public static void GenerateToken(this LoginDto login)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            IdentityModelEventSource.ShowPII = true;
            var key = Encoding.UTF8.GetBytes(Secret);
            
            var roles = new List<Claim>(login.Roles.Select(r => new Claim(ClaimTypes.Role, r.RoleName)));
            roles.Add(new Claim(ClaimTypes.Name, login.UserId));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(roles),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            login.AccessToken = tokenHandler.WriteToken(token);
            login.Expiration = token.ValidTo;

        }

        public static void AddBearerTokenValidation(this IServiceCollection  serviceCollection, string key)
        {
            serviceCollection
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            Secret = key;
        }
    }
}