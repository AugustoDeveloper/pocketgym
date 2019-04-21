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
        private readonly ISerieRepository serieRepository;

        public SerieApplicationService(ISerieRepository serieRepository, IMapper mapper) : base(mapper)
        {
            this.serieRepository = serieRepository;
            RegisterDisposable(serieRepository);
        }

        public async Task<SerieDto> AddAsync(SerieDto serie)
        {
            if (await serieRepository.ExistsByAsync(s => s.Name == serie.Name))
            {
                throw new ValueAlreadyRegisteredException(serie.Name);
            }

            var serieEntity = serie.ToEntity<Serie>(Mapper);
            serieEntity = await serieRepository.AddAsync(serieEntity);
            return serieEntity.ToDto<SerieDto>(Mapper);
        }

        public Task<bool> DeleteAsync(SerieDto serie)
        {
            return serieRepository.DeleteAsync(serie.ToEntity<Serie>(Mapper));
        }

        public async Task<SerieDto> GetByIdAsync(long id)
        {
            var serieEntity = await serieRepository.GetByAsync(s => s.Id == id);
            return serieEntity.ToDto<SerieDto>(Mapper);
        }

        public async Task<IEnumerable<SerieDto>> LoadAllByUserIdAsync(long currentUserId)
        {
            var list = await serieRepository.LoadByAsync(s => s.UserId == currentUserId);
            if (list == null)
            {
                return null;
            }

            return list.Select(s => s.ToDto<SerieDto>(Mapper)).ToList();
        }

        public async Task<SerieDto> UpdateAsync(SerieDto serie)
        {
            if (await serieRepository.ExistsByAsync(s => s.Name == serie.Name))
            {
                throw new ValueAlreadyRegisteredException(serie.Name);
            }

            var serieEntity = await serieRepository.UpdateAsync(serie.ToEntity<Serie>(Mapper));
            return serieEntity.ToDto<SerieDto>(Mapper);
        }
    }
}