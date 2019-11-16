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

            CreateMap<Exercise, ExerciseDto>();
            CreateMap<ExerciseDto, Exercise>();

            CreateMap<Serie, SerieDto>();
            CreateMap<SerieDto, Serie>()
                .ForMember(s => s.Exercises, options => options.Ignore());
        }
    }
}