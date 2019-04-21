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
                .ForMember(u => u.PasswordSalt, options => options.Ignore())
                .ForMember(u => u.Role, options => options.Ignore());
            CreateMap<User, UserDto>()
                .ForMember(u => u.Password, options => options.Ignore());

            CreateMap<Exercise, ExerciseDto>();
            CreateMap<ExerciseDto, Exercise>();

            CreateMap<ExerciseSerie, ExerciseSerieDto>()
                .ForMember(es => es.Exercises,
                    options => options.MapFrom(
                        src => src.Exercises)
                );
            CreateMap<ExerciseSerieDto, ExerciseSerie>()
                .ForMember(es => es.Exercises,
                    options => options.MapFrom(
                        src => src.Exercises)
                );

            CreateMap<Serie, SerieDto>();
            CreateMap<SerieDto, Serie>()
                .ForMember(s => s.ExercisesSeries, options => options.Ignore());
        }
    }
}