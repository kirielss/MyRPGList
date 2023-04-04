using Microsoft.AspNetCore.Mvc;
using MyRPGList.Models;

namespace MyRPGList.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{

    private static List<Game> games = new List<Game>();
    private static int id = 0;

    [HttpPost]
    public void AddGame([FromBody] Game game)
    {
        game.Id = id++;
        games.Add(game);
        Console.WriteLine(game.Name);
        Console.WriteLine(game.Developer);
    }

    [HttpGet]
    public IEnumerable<Game> GetAllGames([FromQuery]int skip = 0, [FromQuery]int take = 20)
    {
        return games.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public Game? GetGameById(int id)
    {
        return games.FirstOrDefault(game => game.Id == id);
    }
}
