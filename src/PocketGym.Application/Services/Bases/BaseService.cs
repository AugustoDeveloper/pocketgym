using System;
using System.Collections.Generic;
using AutoMapper;
using PocketGym.Application.Core.Dtos;

namespace PocketGym.Application.Services.Bases
{
    public class BaseService<TDto> : IApplicationService<TDto> where TDto : class, IDataTransferObject
    {
        private bool disposed;
        private readonly List<IDisposable> disposeRegistration;
        protected IMapper Mapper { get; private set; }

        protected BaseService(IMapper mapper)
        {
            Mapper = mapper;
            disposeRegistration = new List<IDisposable>();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !disposed)
            {
                Mapper = null;
                disposeRegistration.ForEach(d => d.Dispose());
                disposed = true;
            }
        }

        protected void RegisterDisposable(IDisposable disposable)
        {
            disposeRegistration.Add(disposable);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
