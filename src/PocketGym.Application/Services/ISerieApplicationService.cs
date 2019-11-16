using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PocketGym.Application.Core.Dtos;
using PocketGym.Application.Services.Bases;

namespace PocketGym.Application.Services
{
    public interface ISerieApplicationService : IApplicationService<SerieDto>
    {
        Task<SerieDto> AddAsync(string userId, SerieDto serie);
        Task<SerieDto> GetByIdAsync(string userId, string serieId);
        Task<SerieDto> UpdateAsync(string userId, SerieDto serie);
        Task<bool> DeleteAsync(string userId, string id);
        Task<IEnumerable<SerieDto>> LoadAllByUserIdAsync(string currentUserId);
    }
}