using System;
using AutoMapper;
using PocketGym.Application.Core.Dtos;
using PocketGym.Domain.Core.Entities;

namespace PocketGym.Infrastructure.CrossCutting.Mappings
{
    public static class MappingExtension
    {
        public static IExerciseStep ToEntity(this IExerciseStepDto dto, IMapper mapper)
            => dto is SingleExerciseStepDto ? mapper.Map<SingleExerciseStep>(dto) :
                dto is ConjugateExerciseStepDto ? (IExerciseStep) mapper.Map<ConjugateExerciseStep>(dto) : 
                dto is DropSetExerciseStepDto ? mapper.Map<DropSetExerciseStep>(dto) : 
                throw new ArgumentException(nameof(dto));

        public static Serie ToEntity(this SerieDto dto, IMapper mapper) 
            => mapper.Map<Serie>(dto);

        public static TEntity ToEntity<TEntity>(this IDataTransferObject dto, IMapper mapper) where TEntity : class, IEntity
        {
            return mapper.Map<TEntity>(dto);
        }

        public static IExerciseStepDto ToDto(this IExerciseStep entity, IMapper mapper)
            => entity is SingleExerciseStep ? mapper.Map<SingleExerciseStepDto>(entity) :
                entity is ConjugateExerciseStep ? (IExerciseStepDto) mapper.Map<ConjugateExerciseStepDto>(entity) :
                entity is DropSetExerciseStep ? mapper.Map<DropSetExerciseStepDto>(entity) : 
                throw new ArgumentException(nameof(entity));

        public static SerieDto ToDto(this Serie entity, IMapper mapper) 
            => mapper.Map<SerieDto>(entity);
        public static TDto ToDto<TDto>(this IEntity entity, IMapper mapper) where TDto : class, IDataTransferObject
        {
            return mapper.Map<TDto>(entity);
        }
    }
}