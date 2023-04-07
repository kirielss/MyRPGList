using AutoMapper;
using MyRPGList.Data.Dtos;
using MyRPGList.Models;

namespace MyRPGList.Profiles;

public class DevProfile : Profile
{
    public DevProfile()
    {
        CreateMap<CreateDevDto, Dev>();
        CreateMap<UpdateDevDto, Dev>();
        CreateMap<Dev, UpdateDevDto>();
        CreateMap<Dev, ReadDevDto>();
    }
}
