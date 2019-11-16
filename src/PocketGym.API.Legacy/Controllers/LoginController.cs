using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PocketGym.API.Configurations;
using PocketGym.Application.Core.Dtos;
using PocketGym.Application.Services;

namespace PocketGym.API.Controllers
{
    [Authorize, ApiController, Route("api/[controller]")]
    public class LoginController : BaseApiController
    {
        private readonly IUserApplicationService userService;

        public LoginController(IUserApplicationService userService, IOptions<AppSettings> settings) : base(settings.Value)
        {
            this.userService = userService;
        }

        [AllowAnonymous, HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] LoginDto login, [FromServices]SigninConfigurations signinConfigurations)
        {
            if(string.IsNullOrWhiteSpace(login.Username) ||
                string.IsNullOrWhiteSpace(login.Password))
            {
                return BadRequest(new { Reason = "Username or Password is empty" });
            }

            var user = await userService.AuthenticateAsync(login);
            if (user == null)
            {
                return Unauthorized(new { Reason = "Username or Password is incorrect" });
            }

            var role = await userService.GetRoleAsync(user);
            var creation = DateTime.UtcNow;
            var expiration = creation.AddDays(7);

            return Ok(new
            {
                Created = creation,
                Expiration = expiration,
                AccessToken = GenerateToken(user, role, signinConfigurations, creation, expiration)
            });
        }

        protected string GenerateToken(UserDto user, 
            RoleDto role, 
            SigninConfigurations signinConfigurations, 
            DateTime creation, 
            DateTime expiration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(Settings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new GenericIdentity(user.Id.ToString(), "Login"),
                new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, role.RoleName)
                }),
                Issuer = Settings.JWT.Issuer,
                Audience = Settings.JWT.Audience,
                Expires = expiration,
                NotBefore = creation,
                SigningCredentials = signinConfigurations.SigningCredentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}