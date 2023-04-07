using AutoMapper;
using MyRPGList.Data.Dtos;
using MyRPGList.Models;

namespace MyRPGList.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDto, Dev>();
        CreateMap<UpdateUserDto, Dev>();
        CreateMap<Dev, UpdateUserDto>();
        CreateMap<Dev, ReadUserDto>();
    }
}
