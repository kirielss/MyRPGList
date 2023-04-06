using AutoMapper;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MyRPGList.Data;
using MyRPGList.Data.Dtos;
using MyRPGList.Data.DTOs;
using MyRPGList.Models;

namespace MyRPGList.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{

    private MyRpgListDbContext _dbContext;
    private IMapper _mapper;

    public GameController(MyRpgListDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um jogo ao banco de dados
    /// </summary>
    /// <param name="gameDto">Objeto com os campos necessários para criação de um jogo</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AddGame([FromBody] CreateGameDto gameDto)
    {

        Game game = _mapper.Map<Game>(gameDto);
        _dbContext.Games.Add(game);
        _dbContext.SaveChanges();
        return CreatedAtAction(nameof(GetGameById), new {id = game.Id}, game);
    }

    /// <summary>
    /// Pesquisa uma quantidade de jogos no banco de dados. Valor padrão: 20 itens.
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso pesquisa seja feita com sucesso</response>
    [HttpGet]
    public IEnumerable<ReadGameDto> GetAllGames([FromQuery]int skip = 0, [FromQuery]int take = 20)
    {
        return _mapper.Map<List<ReadGameDto>>(_dbContext.Games.Skip(skip).Take(take));
    }

    /// <summary>
    /// Pesquisa um jogo no banco de dados
    /// </summary>
    /// <param name="id">Inteiro com o Id para encontrar o jogo</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso pesquisa seja feita com sucesso</response>
    [HttpGet("{id}")]
    public IActionResult GetGameById(int id)
    {
        var game = _dbContext.Games.FirstOrDefault(game => game.Id == id);
        if (game == null) return NotFound();
        var gameDto = _mapper.Map<ReadGameDto>(game);
        return Ok(gameDto);
    }

    /// <summary>
    /// Edita um jogo no banco de dados.
    /// </summary>
    /// <param name="id">Inteiro com o Id para encontrar o jogo</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso pesquisa seja feita com sucesso</response>
    [HttpPut("{id}")]
    public IActionResult UpdateGame(int id, [FromBody] UpdateGameDto gameDto)
    {
        var game = _dbContext.Games.FirstOrDefault(game => game.Id == id);
        if (game == null) return NotFound();
        _mapper.Map(gameDto, game);
        _dbContext.SaveChanges();
        return NoContent();

    }

    /// <summary>
    /// Edita um dado de um jogo no banco de dados.
    /// </summary>
    /// <param name="id">Inteiro com o Id para encontrar o jogo</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso pesquisa seja feita com sucesso</response>
    [HttpPatch("{id}")]
    public IActionResult UpdateGamePatch(int id, JsonPatchDocument<UpdateGameDto> patch)
    {
        var game = _dbContext.Games.FirstOrDefault(game => game.Id == id);
        if (game == null) return NotFound();

        var GameToUpdate = _mapper.Map<UpdateGameDto>(game);

        patch.ApplyTo(GameToUpdate, ModelState);

        if (!TryValidateModel(GameToUpdate))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(GameToUpdate, game);
        _dbContext.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Deleta um jogo no banco de dados.
    /// </summary>
    /// <param name="id">Inteiro com o Id para encontrar o jogo</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso pesquisa seja feita com sucesso</response>
    [HttpDelete("{id}")]
    public IActionResult DeleteGame(int id)
    {
        var game = _dbContext.Games.FirstOrDefault(game => game.Id == id);
        if (game == null) return NotFound();
        _dbContext.Remove(game);
        _dbContext.SaveChanges();
        return NoContent();
    }
}
