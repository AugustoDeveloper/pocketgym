using System;
using PocketGym.Application.Core.Dtos;

namespace PocketGym.Application.Services
{
    public interface IApplicationService<TDto> : IDisposable where TDto : class, IDataTransferObject
    {

    }
}