using System;
using System.Threading.Tasks;
using PocketGym.Application.Core.Dtos;

namespace PocketGym.Application.Services
{
    public interface IUserApplicationService : IApplicationService<UserDto>
    {
        Task<UserDto> CreateUserAsync(UserDto user);
        Task<UserDto> GetUserByIdAsync(long id);
    }
}