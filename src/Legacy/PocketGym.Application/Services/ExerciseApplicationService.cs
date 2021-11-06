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
        private readonly IExerciseRepository repository;

        public ExerciseApplicationService(IExerciseRepository repository, IMapper mapper) : base(mapper)
        {
            this.repository = repository;
        }

        public async Task<ExerciseDto> AddAsync(ExerciseDto exercise)
        {
            var registeredExercise = await repository.GetByAsync(u => u.Name == exercise.Name);
            if (registeredExercise != null)
            {
                throw new ValueAlreadyRegisteredException(exercise.Name);
            }

            registeredExercise = await repository.AddAsync(exercise.ToEntity<Exercise>(Mapper));
            return registeredExercise.ToDto<ExerciseDto>(Mapper);
        }

        public Task<bool> DeleteByAsync(string id) => repository.DeleteAsync(e => e.Id == id);

        public async Task<ExerciseDto> GetByIdAsync(string id)
        {
            var registeredExercise = await repository.GetByAsync(e => e.Id == id);
            return registeredExercise.ToDto<ExerciseDto>(Mapper);
        }

        public async Task<IEnumerable<ExerciseDto>> LoadAllAsync()
        {
            var exercises = await repository.LoadAllAsync();
            return exercises.Select(e => e.ToDto<ExerciseDto>(Mapper)).ToList();
        }
    }
}