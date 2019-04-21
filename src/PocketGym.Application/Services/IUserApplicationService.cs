using System;
using System.Threading.Tasks;
using PocketGym.Application.Core.Dtos;
using PocketGym.Application.Services.Bases;

namespace PocketGym.Application.Services
{
    public interface IUserApplicationService : IApplicationService<UserDto>
    {
        Task<UserDto> CreateUserAsync(UserDto user, string roleName = "user");
        Task<UserDto> GetUserByIdAsync(long id);
        Task<UserDto> AuthenticateAsync(LoginDto login);
        Task<RoleDto> GetRoleAsync(UserDto user);
    }
}