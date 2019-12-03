using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PocketGym.Application.Core.Dtos;
using PocketGym.Application.Services.Bases;

namespace PocketGym.Application.Services
{
    public interface IExerciseStepApplicationService : IApplicationService<IExerciseStepDto>
    {
        Task<IExerciseStepDto> AddAsync(string userId, string targetId, string serieId, IExerciseStepDto exercise);
        Task<IExerciseStepDto> GetByIdAsync(string userId, string targetId, string serieId, string id);
        Task<IEnumerable<IExerciseStepDto>> LoadAllAsync(string userId, string targetId, string serieId);
        Task<bool> DeleteByAsync(string userId, string targetId, string serieId, string id);
    }
}