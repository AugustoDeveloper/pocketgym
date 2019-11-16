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

        public async Task<SerieDto> AddAsync(string userId, SerieDto serie)
        {
            var registeredUser = await userRepository.GetByAsync(u => u.Id == userId);

            if (registeredUser.Series.Any(s => s.Id == serie.Id))
            {
                throw new ValueAlreadyRegisteredException(serie.Id);
            }

            var serieEntity = serie.ToEntity<Serie>(Mapper);
            registeredUser.Series.Add(serieEntity);
            await userRepository.UpdateAsync(registeredUser);
            return serieEntity.ToDto<SerieDto>(Mapper);
        }

        public async Task<bool> DeleteAsync(string userId, string id)
        {
            var registeredUser = await userRepository.GetByAsync(u => u.Id == userId);

            var serieToRemove = registeredUser.Series.FirstOrDefault(s => s.Id == id);
            
            if (serieToRemove == null)
            {
                return false;
            }

            registeredUser.Series.Remove(serieToRemove);

            await userRepository.UpdateAsync(registeredUser);

            return true;
        }

        public async Task<SerieDto> GetByIdAsync(string userId, string serieId)
        {
            var registeredUser = await userRepository.GetByAsync(u => u.Id == userId);
            return registeredUser.Series.FirstOrDefault(s => s.Id == serieId).ToDto<SerieDto>(Mapper);
        }

        public async Task<IEnumerable<SerieDto>> LoadAllByUserIdAsync(string currentUserId)
        {
            var registeredUser = await userRepository.GetByAsync(u => u.Id == currentUserId);
            return registeredUser.Series.Select(s => s.ToDto<SerieDto>(Mapper)).ToArray();
        }

        public async Task<SerieDto> UpdateAsync(string userId, SerieDto serie)
        {
            var registeredUser = await userRepository.GetByAsync(u => u.Id == userId);

            var serieToUpdate = registeredUser.Series.FirstOrDefault(s => s.Id == serie.Id);

            if (serieToUpdate == null)
            {
                return null;
            }

            serieToUpdate.Title = serie.Title;
            serieToUpdate.RestTimeBetweenExercisesInSeconds = serie.RestTimeBetweenExercisesInSeconds;
            await userRepository.UpdateAsync(registeredUser);

            return serieToUpdate.ToDto<SerieDto>(Mapper);
        }
    }
}