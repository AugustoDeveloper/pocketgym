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
    public class SerieApplicationService : BaseService<SerieDto>, ISerieApplicationService
    {
        private readonly IUserRepository userRepository;

        public SerieApplicationService(IUserRepository userRepository, IMapper mapper) : base(mapper)
        {
            this.userRepository = userRepository;
        }

        public async Task<SerieDto> AddAsync(string userId, string targetId, SerieDto serie)
        {
            var registeredUser = await userRepository.GetByAsync(u => u.Id == userId);
            var target = registeredUser.Targets.FirstOrDefault(t => t.Id == targetId);

            if (target.Series.Any(s => s.Id == serie.Id))
            {
                throw new ValueAlreadyRegisteredException(serie.Id.ToString());
            }

            var serieEntity = serie.ToEntity(Mapper);
            target.Series.Add(serieEntity);
            await userRepository.UpdateAsync(registeredUser);
            return serieEntity.ToDto(Mapper);
        }

        public async Task<bool> DeleteAsync(string userId, string targetId, string id)
        {
            var registeredUser = await userRepository.GetByAsync(u => u.Id == userId);
            var target = registeredUser.Targets.FirstOrDefault(t => t.Id == targetId);

            var serieToRemove = target.Series.FirstOrDefault(s => s.Id == id);
            
            if (serieToRemove == null)
            {
                return false;
            }

            target.Series.Remove(serieToRemove);

            await userRepository.UpdateAsync(registeredUser);

            return true;
        }

        public async Task<SerieDto> GetByIdAsync(string userId, string targetId, string serieId)
        {
            var registeredUser = await userRepository.GetByAsync(u => u.Id == userId);
            var target = registeredUser.Targets.FirstOrDefault(t => t.Id == targetId);

            return target.Series.FirstOrDefault(s => s.Id == serieId).ToDto(Mapper);
        }

        public async Task<IEnumerable<SerieDto>> LoadAllByUserIdAsync(string currentUserId, string targetId)
        {
            var registeredUser = await userRepository.GetByAsync(u => u.Id == currentUserId);
            var target = registeredUser.Targets.FirstOrDefault(t => t.Id == targetId);

            return target.Series.Select(s => s.ToDto(Mapper)).ToArray();
        }
    }
}