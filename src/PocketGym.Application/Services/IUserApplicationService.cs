using System;
using System.Threading.Tasks;
using PocketGym.Application.Core.Dtos;
using PocketGym.Application.Services.Bases;

namespace PocketGym.Application.Services
{
    public interface IUserApplicationService : IApplicationService<UserDto>
    {
        Task<UserDto> CreateUserAsync(UserDto user);
        Task<UserDto> GetUserByIdAsync(string id);
        Task AuthenticateAsync(LoginDto login);
        Task<UserDto> UpdateAsync(UserDto user);
    }
}