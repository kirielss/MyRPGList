using Microsoft.AspNetCore.Mvc;
using MyRPGList.Data;
using MyRPGList.Models;

namespace MyRPGList.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{

    //private static List<Game> games = new List<Game>();
    //private static int id = 0;
    // these lines doesnt apply anymore as now controller needs to pickup data from the DB
    // agora pra funcionar precisa do contexto que vai ser responsável por acessar o banco de dados
    // essa dependência do contexto enseja na famigerada INJEÇÃO DE DEPENDÊNCIA

    private MyRpgListDbContext _dbContext;

    // injeção de dependência
    public GameController(MyRpgListDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public IActionResult AddGame([FromBody] Game game)
    {
        _dbContext.Games.Add(game);
        _dbContext.SaveChanges();
        return CreatedAtAction(nameof(GetGameById), new {id = game.Id}, game);
    }

    [HttpGet]
    public IEnumerable<Game> GetAllGames([FromQuery]int skip = 0, [FromQuery]int take = 20)
    {
        return _dbContext.Games.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult GetGameById(int id)
    {
        var game = _dbContext.Games.FirstOrDefault(game => game.Id == id);
        if (game == null) return NotFound();
        return Ok();
    }
}
