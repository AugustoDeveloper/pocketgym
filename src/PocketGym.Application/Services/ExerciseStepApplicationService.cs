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
    public class ExerciseStepApplicationService : BaseService<ExerciseDto>, IExerciseStepApplicationService
    {
        private readonly IUserRepository repository;

        public ExerciseStepApplicationService(IUserRepository repository, IMapper mapper) : base(mapper)
        {
            this.repository = repository;
        }

        public async Task<IExerciseStepDto> AddAsync(string userId, string targetId, string serieId, IExerciseStepDto exercise)
        {
            var user = await repository.GetByAsync(u => u.Id == userId);
            var target = user.Targets.FirstOrDefault(t => t.Id == targetId);
            var userSerie = target.Series.FirstOrDefault(s => s.Id == serieId);

            var exerciseToUpdate = exercise.ToEntity(Mapper);
            exerciseToUpdate.Id = repository.GenerateId();

            userSerie.Steps.Add(exerciseToUpdate);

            await repository.UpdateAsync(user);

            return exerciseToUpdate.ToDto(Mapper);
        }

        public async Task<bool> DeleteByAsync(string userId, string targetId, string serieId, string id)
        {
            var user = await repository.GetByAsync(u => u.Id == userId);
            var target = user.Targets.FirstOrDefault(t => t.Id == targetId);
            var userSerie = target.Series.FirstOrDefault(s => s.Id == serieId);

            var exerciseSerieToRemove = userSerie.Steps.FirstOrDefault(es => es.Id == id);
            userSerie.Steps.Remove(exerciseSerieToRemove);

            await repository.UpdateAsync(user);

            return true;
        }

        public async Task<IExerciseStepDto> GetByIdAsync(string userId, string targetId, string serieId, string id)
        {
            var user = await repository.GetByAsync(u => u.Id == userId);
            var target = user.Targets.FirstOrDefault(t => t.Id == targetId);
            var userSerie = target.Series.FirstOrDefault(s => s.Id == serieId);

            var exerciseSerie = userSerie.Steps.FirstOrDefault(es => es.Id == id);

            if (exerciseSerie == null)
            {
                return null;
            }

            return exerciseSerie.ToDto(Mapper);
        }

        public async Task<IEnumerable<IExerciseStepDto>> LoadAllAsync(string userId, string targetId, string serieId)
        {
            var user = await repository.GetByAsync(u => u.Id == userId);
            var target = user.Targets.FirstOrDefault(t => t.Id == targetId);
            var userSerie = target.Series.FirstOrDefault(s => s.Id == serieId);
            var list = userSerie.Steps.Select(s => s.ToDto(Mapper)).ToList();
            return list;
        }
    }
}