using AutoMapper;
using MyRPGList.Data.DTOs;
using MyRPGList.Models;

namespace MyRPGList.Profiles;

public class GameProfile : Profile
{
    public GameProfile()
    {
        CreateMap<CreateGameDto, Game>();
    }
}
