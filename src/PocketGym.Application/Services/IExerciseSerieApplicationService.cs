using System.Collections.Generic;
using System.Threading.Tasks;
using PocketGym.Application.Core.Dtos;
using PocketGym.Application.Services.Bases;

namespace PocketGym.Application.Services
{
    public interface IExerciseSerieApplicationService : IApplicationService<ExerciseSerieDto>
    {
        Task<IEnumerable<ExerciseSerieDto>> LoadAllBySerieAsync(SerieDto serie);
        Task<ExerciseSerieDto> GetByIdAsync(SerieDto serie, long id);
        Task<bool> DeleteByIdAsync(SerieDto serie, long id);
        Task<ExerciseSerieDto> UpdateAsync(SerieDto serie, ExerciseSerieDto exerciseSerieDto);
        Task<ExerciseSerieDto> AddAsync(SerieDto serie, ExerciseSerieDto exerciseSerieDto);
    }
}