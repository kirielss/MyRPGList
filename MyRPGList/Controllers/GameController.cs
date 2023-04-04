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
    public IEnumerable<Game> GetAllGames()
    {
        return games;
    }

    [HttpGet("{id}")]
    public Game? GetGameById(int id)
    {
        return games.FirstOrDefault(game => game.Id == id);
    }
}
