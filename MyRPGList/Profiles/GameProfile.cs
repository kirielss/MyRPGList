using AutoMapper;
using MyRPGList.Data;
using MyRPGList.Data.Dtos;
using MyRPGList.Data.DTOs;
using MyRPGList.Models;

namespace MyRPGList.Profiles;

public class GameProfile : Profile
{
    private MyRpgListDbContext _dbContext;

    public GameProfile(MyRpgListDbContext dbContext)
    {
        _dbContext = dbContext;

        CreateMap<CreateGameDto, Game>()
            .ForMember(g => g.Developer, opt => opt.MapFrom(MapDeveloper));

        CreateMap<UpdateGameDto, Game>();
        CreateMap<Game, UpdateGameDto>();
        CreateMap<Game, ReadGameDto>();
    }

    private Dev MapDeveloper(CreateGameDto dto, Game game)
    {
        var dev = _dbContext.Developers.SingleOrDefault(d => d.Name == dto.Name);
        if (dev == null)
        {
            dev = new Dev { Name = dto.Name };
            _dbContext.Developers.Add(dev);
        }
        return dev;

    }


}