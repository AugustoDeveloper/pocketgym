using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PocketGym.Application.Core.Dtos;
using PocketGym.Application.Services.Bases;

namespace PocketGym.Application.Services
{
    public interface IExerciseApplicationService : IApplicationService<ExerciseDto>
    {
        Task<ExerciseDto> AddAsync(ExerciseDto exercise);
        Task<ExerciseDto> GetByIdAsync(string id);
        Task<IEnumerable<ExerciseDto>> LoadAllAsync();
        Task<bool> DeleteByAsync(string id);
    }
}