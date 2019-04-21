using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PocketGym.Application.Core.Dtos;
using PocketGym.Application.Exceptions;
using PocketGym.Application.Services.Bases;
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
            RegisterDisposable(userRepository);
        }

        public async Task<UserDto> AuthenticateAsync(LoginDto login)
        {
            var existsUser = await userRepository.GetByAsync(u => u.Username == login.Username);
            if (existsUser == null)
            {
                return null;
            }

            if (!this.VerifyPasswordHash(login.Password, existsUser.PasswordHash, existsUser.PasswordSalt))
            {
                return null;
            }

            return existsUser.ToDto<UserDto>(Mapper);
        }

        public async Task<UserDto> CreateUserAsync(UserDto user, string roleName = "User")
        {
            var password = user.Password;
            user.Password = null;

            if (await userRepository.ExistsByAsync(u => u.Username == user.Username))
            {
                throw new ValueAlreadyRegisteredException(user.Username);
            }

            var entityUser = user.ToEntity<User>(Mapper);
            entityUser.Role = new Role { RoleName = roleName };

            var hmac = this.CreatePasswordHash(password);
            entityUser.PasswordSalt = hmac.PasswordSalt;
            entityUser.PasswordHash = hmac.PasswordHash;

            entityUser = await userRepository.AddAsync(entityUser);

            return entityUser.ToDto<UserDto>(Mapper);
        }

        public async Task<UserDto> GetUserByIdAsync(long id)
        {
            var entityUser = await userRepository.GetByAsync(u => u.Id == id);

            return entityUser.ToDto<UserDto>(Mapper);
        }

        protected (byte[] PasswordHash, byte[] PasswordSalt) CreatePasswordHash(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                return (hmac.Key, hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }

        protected bool VerifyPasswordHash(string password, byte[] hash, byte[] salt)
        {
            using (var hmac = new HMACSHA512(salt))
            {
                byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] == hash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public async Task<RoleDto> GetRoleAsync(UserDto user)
        {
            var userEntity = await userRepository.GetByAsync(u => u.Id == user.Id);
            return new RoleDto { RoleName = userEntity.Role.RoleName };
        }
    }
}