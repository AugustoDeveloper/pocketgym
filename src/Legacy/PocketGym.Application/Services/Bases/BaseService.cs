using System;
using System.Collections.Generic;
using AutoMapper;
using PocketGym.Application.Core.Dtos;

namespace PocketGym.Application.Services.Bases
{
    public class BaseService<TDto> : IApplicationService<TDto> where TDto : class, IDataTransferObject
    {
        protected bool Disposed { get; private set; }
        protected IMapper Mapper { get; private set; }

        protected BaseService(IMapper mapper)
        {
            Mapper = mapper;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !Disposed)
            {
                Mapper = null;
                Disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
