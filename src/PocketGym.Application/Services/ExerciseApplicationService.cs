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
        private readonly IUserRepository repository;
        public ExerciseApplicationService(IUserRepository repository, IMapper mapper) : base(mapper)
        {
            this.repository = repository;
        }

        public async Task<ExerciseDto> AddAsync(string userId, string serieId, ExerciseDto exercise)
        {
            var user = await repository.GetByAsync(u => u.Id == userId);
            var userSerie = user.Series.FirstOrDefault(s => s.Id == serieId);

            var exerciseToUpdate = exercise.ToEntity<Exercise>(Mapper);
            exerciseToUpdate.Id = repository.GenerateId();

            userSerie.Exercises.Add(exerciseToUpdate);

            await repository.UpdateAsync(user);

            return exerciseToUpdate.ToDto<ExerciseDto>(Mapper);
        }

        public async Task<bool> DeleteByAsync(string userId, string serieId, string id)
        {
            var user = await repository.GetByAsync(u => u.Id == userId);
            var userSerie = user.Series.FirstOrDefault(s => s.Id == serieId);

            var exerciseSerieToRemove = userSerie.Exercises.FirstOrDefault(es => es.Id == id);
            userSerie.Exercises.Remove(exerciseSerieToRemove);

            await repository.UpdateAsync(user);

            return true;
        }

        public async Task<ExerciseDto> GetByIdAsync(string userId, string serieId, string id)
        {
            var user = await repository.GetByAsync(u => u.Id == userId);
            var userSerie = user.Series.FirstOrDefault(s => s.Id == serieId);

            var exerciseSerie = userSerie.Exercises.FirstOrDefault(es => es.Id == id);

            if (exerciseSerie == null)
            {
                return null;
            }

            return exerciseSerie.ToDto<ExerciseDto>(Mapper);
        }

        public async Task<IEnumerable<ExerciseDto>> LoadAllAsync(string userId, string serieId)
        {
            var user = await repository.GetByAsync(u => u.Id == userId);
            var userSerie = user.Series.FirstOrDefault(s => s.Id == serieId);
            var list = userSerie.Exercises.Select(s => s.ToDto<ExerciseDto>(Mapper)).ToList();
            return list;
        }

        public async Task<ExerciseDto> UpdateAsync(string userId, string serieId, ExerciseDto exercise)
        {
            var user = await repository.GetByAsync(u => u.Id == userId);
            var userSerie = user.Series.FirstOrDefault(s => s.Id == serieId);

            var exerciseRegistered = userSerie.Exercises.FirstOrDefault(e => e.Id == exercise.Id);
            if (exerciseRegistered == null)
            {
                return null;
            }
            exerciseRegistered.Name = exercise.Name;
            exerciseRegistered.NumberOfRepetitions = exercise.NumberOfRepetitions;
            exerciseRegistered.NumberOfExerciseRepetitions = exercise.NumberOfExerciseRepetitions;
            exerciseRegistered.RestTimeInSeconds = exercise.RestTimeInSeconds;
            
            await repository.UpdateAsync(user);

            return exerciseRegistered.ToDto<ExerciseDto>(Mapper);
        }
    }
}