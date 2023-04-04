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
    public IActionResult AddGame([FromBody] Game game)
    {
        game.Id = id++;
        games.Add(game);
        return CreatedAtAction(nameof(GetGameById), new {id = game.Id}, game);
    }

    [HttpGet]
    public IEnumerable<Game> GetAllGames([FromQuery]int skip = 0, [FromQuery]int take = 20)
    {
        return games.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult GetGameById(int id)
    {
        var game = games.FirstOrDefault(game => game.Id == id);
        if (game == null) return NotFound();
        return Ok();
    }
}
