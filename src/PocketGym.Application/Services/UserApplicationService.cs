using System;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PocketGym.Application.Core.Dtos;
using PocketGym.Domain.Core.Entities;
using PocketGym.Domain.Repositories;
using PocketGym.Infrastructure.CrossCutting.Mappings;

namespace PocketGym.Application.Services
{
    public class UserApplicationService : BaseService<UserDto>, IUserApplicationService
    {
        private readonly IUserRepository userRepository;

        public UserApplicationService(IUserRepository userRepository, IMapper mapper) : base(mapper)
        {
            this.userRepository = userRepository;
        }

        public async Task<UserDto> CreateUserAsync(UserDto user)
        {
            if (await userRepository.ExistsByAsync(u => u.Username == user.Username))
            {
                return null;
            }

            user.Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(user.Password));
            var entityUser = user.ToEntity<User>(Mapper);

            entityUser = await userRepository.AddAsync(entityUser);

            return entityUser.ToDto<UserDto>(Mapper);
        }

        public async Task<UserDto> GetUserByIdAsync(long id)
        {
            var entityUser = await userRepository.GetByIdAsync(id);
            return entityUser.ToDto<UserDto>(Mapper);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                userRepository?.Dispose();
            }
        }
    }
}