using System;
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
    public class ExerciseApplicationService : BaseService<ExerciseDto>, IExerciseApplicationService
    {
        private readonly IExerciseRepository exerciseRepository;
        public ExerciseApplicationService(IExerciseRepository exerciseRepository, IMapper mapper) : base(mapper)
        {
            this.exerciseRepository = exerciseRepository;
            RegisterDisposable(exerciseRepository);
        }

        public async Task<ExerciseDto> AddAsync(ExerciseDto exercise)
        {
            if (await exerciseRepository.ExistsByAsync(e => e.Name == exercise.Name))
            {
                throw new ValueAlreadyRegisteredException(exercise.Name);
            }

            var exerciseEntity = exercise.ToEntity<Exercise>(Mapper);
            exerciseEntity = await exerciseRepository.AddAsync(exerciseEntity);
            return exerciseEntity.ToDto<ExerciseDto>(Mapper);
        }

        public Task<bool> DeleteAsync(ExerciseDto exercise)
        {
            return exerciseRepository.DeleteAsync(exercise.ToEntity<Exercise>(Mapper));
        }

        public async Task<ExerciseDto> GetByIdAsync(long id)
        {
            var exerciseEntity = await exerciseRepository.GetByAsync(e => e.Id == id);
            return exerciseEntity.ToDto<ExerciseDto>(Mapper);
        }

        public async Task<IEnumerable<ExerciseDto>> LoadAllAsync()
        {
            var list = await exerciseRepository.LoadAllAsync();
            if (list == null)
            {
                return null;
            }

            return list.Select(e => e.ToDto<ExerciseDto>(Mapper)).ToList();
        }

        public async Task<ExerciseDto> UpdateAsync(ExerciseDto exercise)
        {
            var serieEntity = await exerciseRepository.UpdateAsync(exercise.ToEntity<Exercise>(Mapper));
            return serieEntity.ToDto<ExerciseDto>(Mapper);
        }
    }
}