using System;
using AutoMapper;
using PocketGym.Application.Core.Dtos;
using PocketGym.Domain.Core.Entities;

namespace PocketGym.Infrastructure.CrossCutting.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}