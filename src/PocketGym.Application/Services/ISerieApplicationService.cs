using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PocketGym.Application.Core.Dtos;
using PocketGym.Application.Services.Bases;

namespace PocketGym.Application.Services
{
    public interface ISerieApplicationService : IApplicationService<SerieDto>
    {
        Task<SerieDto> AddAsync(SerieDto serie);
        Task<SerieDto> GetByIdAsync(long id);
        Task<SerieDto> UpdateAsync(SerieDto serie);
        Task<bool> DeleteAsync(SerieDto serie);
        Task<IEnumerable<SerieDto>> LoadAllByUserIdAsync(long currentUserId);
    }
}