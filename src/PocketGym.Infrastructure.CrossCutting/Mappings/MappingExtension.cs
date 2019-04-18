using AutoMapper;
using PocketGym.Application.Core.Dtos;
using PocketGym.Domain.Core.Entities;

namespace PocketGym.Infrastructure.CrossCutting.Mappings
{
    public static class MappingExtension
    {
        public static TEntity ToEntity<TEntity>(this IDataTransferObject dto, IMapper mapper) where TEntity : class, IEntity
        {
            return mapper.Map<TEntity>(dto);
        }

        public static TDto ToDto<TDto>(this IEntity entity, IMapper mapper) where TDto : class, IDataTransferObject
        {
            return mapper.Map<TDto>(entity);
        }
    }
}