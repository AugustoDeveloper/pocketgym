using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PocketGym.Application.Core.Dtos;
using PocketGym.Application.Services.Bases;

namespace PocketGym.Application.Services
{
    public interface ITargetApplicationService : IApplicationService<TargetDto>
    {
        Task<TargetDto> AddAsync(string userId, TargetDto target);
        Task<TargetDto> GetByIdAsync(string userId, string targetId);
        Task<bool> MarkAsCurrent(string userId, string targetId);
        Task<bool> DeleteAsync(string userId, string targetId);
    }
}