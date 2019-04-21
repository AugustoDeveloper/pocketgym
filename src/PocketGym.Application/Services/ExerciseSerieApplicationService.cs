using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PocketGym.Application.Core.Dtos;
using PocketGym.Application.Services.Bases;
using PocketGym.Domain.Core.Entities;
using PocketGym.Domain.Repositories;
using PocketGym.Infrastructure.CrossCutting.Mappings;

namespace PocketGym.Application.Services
{
    public class ExerciseSerieApplicationService : BaseService<ExerciseSerieDto>, IExerciseSerieApplicationService
    {
        private readonly ISerieRepository serieRepository;
        public ExerciseSerieApplicationService(ISerieRepository repository, IMapper mapper) : base(mapper)
        {
            serieRepository = repository;
            RegisterDisposable(serieRepository);
        }

        public async Task<ExerciseSerieDto> AddAsync(SerieDto serie, ExerciseSerieDto exerciseSerieDto)
        {
            var serieEntity = await serieRepository.GetByAsync(s => s.Id == serie.Id);

            var exerciseSerie = exerciseSerieDto.ToEntity<ExerciseSerie>(Mapper);
            exerciseSerie.Id = serieEntity.ExercisesSeries.Count + 1;
            serieEntity.ExercisesSeries.Add(exerciseSerie);

            await serieRepository.UpdateAsync(serieEntity);

            return exerciseSerie.ToDto<ExerciseSerieDto>(Mapper);
        }

        public async Task<bool> DeleteByIdAsync(SerieDto serie, long id)
        {
            var serieEntity = await serieRepository.GetByAsync(s => s.Id == serie.Id);

            var exercicioSerie = serieEntity.ExercisesSeries.FirstOrDefault(es => es.Id == id);
            if (exercicioSerie == null)
            {
                return false;
            }

            serieEntity.ExercisesSeries.Remove(exercicioSerie);
            await serieRepository.UpdateAsync(serieEntity);

            return true;
        }

        public async Task<ExerciseSerieDto> GetByIdAsync(SerieDto serie, long id)
        {
            var serieEntity = await serieRepository.GetByAsync(s => s.Id == serie.Id);

            var exerciseSerie = serieEntity.ExercisesSeries.FirstOrDefault(es => es.Id == id);
            if (exerciseSerie == null)
            {
                return null;
            }
            return exerciseSerie.ToDto<ExerciseSerieDto>(Mapper);
        }

        public async Task<IEnumerable<ExerciseSerieDto>> LoadAllBySerieAsync(SerieDto serie)
        {
            var serieEntity = await serieRepository.GetByAsync(s => s.Id == serie.Id);
            var list = serieEntity.ExercisesSeries.Select(s => s.ToDto<ExerciseSerieDto>(Mapper)).ToList();
            return list;
        }

        public async Task<ExerciseSerieDto> UpdateAsync(SerieDto serie, ExerciseSerieDto exerciseSerieDto)
        {
            var serieEntity = await serieRepository.GetByAsync(s => s.Id == serie.Id);

            var exerciseSerie = serieEntity.ExercisesSeries.FirstOrDefault(es => es.Id == exerciseSerieDto.Id);
            if (exerciseSerie == null)
            {
                return null;
            }
            var newExerciseSerie = exerciseSerieDto.ToEntity<ExerciseSerie>(Mapper);
            newExerciseSerie.Id = exerciseSerie.Id;
            exerciseSerie = newExerciseSerie;
            await serieRepository.UpdateAsync(serieEntity);

            return exerciseSerie.ToDto<ExerciseSerieDto>(Mapper);
        }
    }
}