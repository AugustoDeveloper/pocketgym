using System.Linq;
using AutoMapper;
using PocketGym.Application.Core.Dtos;
using PocketGym.Domain.Core.Entities;

namespace PocketGym.Infrastructure.CrossCutting.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, User>()
                .ForMember(u => u.PasswordHash, options => options.Ignore())
                .ForMember(u => u.PasswordSalt, options => options.Ignore());
            CreateMap<User, UserDto>()
                .ForMember(u => u.Password, options => options.Ignore());
            
            CreateMap<SingleExerciseStepDto, SingleExerciseStep>()
                .ForMember(u => u.Type, options => options.Ignore());
            CreateMap<SingleExerciseStep, SingleExerciseStepDto>();

            CreateMap<ConjugateExerciseStepDto, ConjugateExerciseStep>()
                .ForMember(u => u.Type, options => options.Ignore());
            CreateMap<ConjugateExerciseStep, ConjugateExerciseStepDto>();

            CreateMap<DropSetExerciseStepDto, DropSetExerciseStep>()
                .ForMember(u => u.Type, options => options.Ignore());
            CreateMap<DropSetExerciseStep, DropSetExerciseStepDto>();

            CreateMap<Target, TargetDto>();
            CreateMap<TargetDto, Target>();

            CreateMap<Exercise, ExerciseDto>();
            CreateMap<ExerciseDto, Exercise>();

            CreateMap<Serie, SerieDto>();
            CreateMap<SerieDto, Serie>()
                .ForMember(s => s.Steps, options => options.Ignore());
        }
    }
}