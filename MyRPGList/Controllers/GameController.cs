using Microsoft.AspNetCore.Mvc;
using MyRPGList.Models;

namespace MyRPGList.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{

    private List<Game> games = new List<Game>();

    [HttpPost]
    public void AddGame([FromBody] Game game)
    {
        games.Add(game);
        Console.WriteLine(game.Name);
        Console.WriteLine(game.Developer);
    }
}
