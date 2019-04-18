using System;
using AutoMapper;
using PocketGym.Application.Core.Dtos;

namespace PocketGym.Application.Services
{
    public class BaseService<TDto> : IApplicationService<TDto> where TDto : class, IDataTransferObject
    {
        private bool disposed;
        protected IMapper Mapper { get; private set; }

        protected BaseService(IMapper mapper)
        {
            Mapper = mapper;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !disposed)
            {
                Mapper = null;
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
