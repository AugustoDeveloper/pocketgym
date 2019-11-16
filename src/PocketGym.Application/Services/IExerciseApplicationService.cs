using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PocketGym.Application.Core.Dtos;
using PocketGym.Application.Services.Bases;

namespace PocketGym.Application.Services
{
    public interface IExerciseApplicationService : IApplicationService<ExerciseDto>
    {
        Task<ExerciseDto> AddAsync(string userId, string serieId, ExerciseDto exercise);
        Task<ExerciseDto> GetByIdAsync(string userId, string serieId, string id);
        Task<IEnumerable<ExerciseDto>> LoadAllAsync(string userId, string serieId);
        Task<bool> DeleteByAsync(string userId, string serieId, string id);
        Task<ExerciseDto> UpdateAsync(string userId, string serieId, ExerciseDto exercise);
    }
}