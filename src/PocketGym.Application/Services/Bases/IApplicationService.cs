using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PocketGym.Application.Core.Dtos;

namespace PocketGym.Application.Services.Bases
{
    public interface IApplicationService<TDto> : IDisposable where TDto : class, IDataTransferObject
    {

    }
}