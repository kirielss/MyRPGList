using AutoMapper;
using MyRPGList.Data.Dtos;
using MyRPGList.Data.DTOs;
using MyRPGList.Models;

namespace MyRPGList.Profiles;

public class GameProfile : Profile
{
    public GameProfile()
    {
        CreateMap<CreateGameDto, Game>();
        CreateMap<UpdateGameDto, Game>();
        CreateMap<Game, UpdateGameDto>();
        CreateMap<Game, ReadGameDto>();
    }
}
