using System.Collections.Generic;
using System.Threading.Tasks;
using PocketGym.Application.Core.Dtos;
using PocketGym.Application.Services.Bases;

namespace PocketGym.Application.Services
{
    public interface IExerciseSerieApplicationService : IApplicationService<ExerciseDto>
    {
        Task<IEnumerable<ExerciseDto>> LoadAllBySerieAsync(string userId, string serieId);
        Task<ExerciseDto> GetByIdAsync(string userId, string serieId, string id);
        Task<bool> DeleteByIdAsync(string userId, string serieId, string id);
        Task<ExerciseDto> UpdateAsync(string userId, string serieId, ExerciseDto exerciseSerieDto);
        Task<ExerciseDto> AddAsync(string userId, string serieId, ExerciseDto exerciseSerieDto);
    }
}