using AutoMapper;
using PocketGym.Application.Core.Dtos;
using PocketGym.Application.Exceptions;
using PocketGym.Application.Extensions;
using PocketGym.Application.Services.Bases;
using PocketGym.Domain.Core.Entities;
using PocketGym.Domain.Repositories;
using PocketGym.Infrastructure.CrossCutting.Mappings;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PocketGym.Application.Services
{
    public class UserApplicationService : BaseService<UserDto>, IUserApplicationService
    {
        private readonly IUserRepository repository;

        public UserApplicationService(IUserRepository userRepository, IMapper mapper) : base(mapper)
        {
            this.repository = userRepository;
        }

        public async Task AuthenticateAsync(LoginDto login)
        {
            var registereUser = await repository.GetByAsync(u => u.Username == login.Username);
            if (registereUser == null)
            {
                throw new ArgumentNullException();
            }

            login.Authenticate(registereUser.PasswordHash, registereUser.PasswordSalt);

            if (registereUser.Roles.Count == 0)
            {
                registereUser.Roles.Add(new Role { RoleName = "User" });
                await repository.UpdateAsync(registereUser);
            }
            login.Roles = registereUser.Roles.Select(r => new RoleDto { RoleName = r.RoleName }).ToArray();
            login.UserId = registereUser.Id;
            login.Password = null;
        }

        public async Task<UserDto> CreateUserAsync(UserDto user)
        {
            user.ValidateToInsert();

            var password = user.Password;
            user.Password = null;

            if (await repository.ExistsByAsync(u => u.Username == user.Username))
            {
                throw new ValueAlreadyRegisteredException(user.Username);
            }

            var entityUser = user.ToEntity<User>(Mapper);
            entityUser.Roles.Add(new Role { RoleName = "User" });

            var hmac = AuthenticationExtension.Encrypt(password);
            entityUser.PasswordSalt = hmac.PasswordSalt;
            entityUser.PasswordHash = hmac.PasswordHash;

            entityUser = await repository.AddAsync(entityUser);

            return entityUser.ToDto<UserDto>(Mapper);
        }

        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            var entityUser = await repository.GetByAsync(u => u.Id == id);

            return entityUser.ToDto<UserDto>(Mapper);
        }        

        public async Task<UserDto> UpdateAsync(UserDto user)
        {
            var registeredUser = await repository.GetByAsync(u => u.Id == user.Id);
            if (registeredUser == null)
            {
                return null;
            }

            registeredUser.Name = user.Name;
            registeredUser.Age = user.Age;

            var updatedUser = await repository.UpdateAsync(registeredUser);
            return updatedUser.ToDto<UserDto>(Mapper);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !Disposed)
            {
                repository.Dispose();
            }
        }
    }
}