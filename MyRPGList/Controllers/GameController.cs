using Microsoft.AspNetCore.Mvc;
using MyRPGList.Models;

namespace MyRPGList.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{

    private static List<Game> games = new List<Game>();

    [HttpPost]
    public void AddGame([FromBody] Game game)
    {
        games.Add(game);
        game.Id = games.Count;
        Console.WriteLine(game.Name);
        Console.WriteLine(game.Developer);
    }

    [HttpGet]
    public List<Game> GetAllGames()
    {
        return games;
    }
}
