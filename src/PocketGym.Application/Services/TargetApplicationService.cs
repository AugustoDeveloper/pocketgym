using System.Collections.Generic;
using System.Linq;
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
    public class TargetApplicationService : BaseService<SerieDto>, ITargetApplicationService
    {
        private readonly IUserRepository repository;
        public TargetApplicationService(IUserRepository userRepository, IMapper mapper) : base(mapper)
        {
            repository = userRepository;
        }

        public async Task<TargetDto> AddAsync(string userId, TargetDto target)
        {
            var registeredUser = await repository.GetByAsync(u => u.Id == userId);

            if (registeredUser.Targets.Any(t => t.Id == target.Id))
            {
                throw new ValueAlreadyRegisteredException(target.Id.ToString());
            }

            var targetEntity = target.ToEntity<Target>(Mapper);
            registeredUser.Targets.Add(targetEntity);
            await repository.UpdateAsync(registeredUser);
            return targetEntity.ToDto<TargetDto>(Mapper);
        }

        public async Task<bool> DeleteAsync(string userId, string targetId)
        {
            var registeredUser = await repository.GetByAsync(u => u.Id == userId);
            var targetToRemove = registeredUser.Targets.FirstOrDefault(t => t.Id == targetId);
            
            if (targetToRemove == null)
            {
                return false;
            }

            registeredUser.Targets.Remove(targetToRemove);

            await repository.UpdateAsync(registeredUser);

            return true;
        }

        public async Task<TargetDto> GetByIdAsync(string userId, string targetId)
        {
            var registeredUser = await repository.GetByAsync(u => u.Id == userId);
            var target = registeredUser.Targets.FirstOrDefault(t => t.Id == targetId);
            return target.ToDto<TargetDto>(Mapper);
        }

        public async Task<bool> MarkAsCurrent(string userId, string targetId)
        {
            var registeredUser = await repository.GetByAsync(u => u.Id == userId);
            var target = registeredUser.Targets.FirstOrDefault(t => t.Id == targetId);
            if (target == null)
            {
                return false;
            }
            registeredUser.CurrentTargetId = target.Id;
            await repository.UpdateAsync(registeredUser);
            return true;
        }
    }
}