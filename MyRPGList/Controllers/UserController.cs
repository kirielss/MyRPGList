using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MyRPGList.Data.Dtos;
using MyRPGList.Data;
using MyRPGList.Models;

namespace MyRPGList.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private MyRpgListDbContext _dbContext;
    private IMapper _mapper;

    public UserController(MyRpgListDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um usuário ao banco de dados
    /// </summary>
    /// <param name="userDto">Objeto com os campos necessários para criação de um usuário</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    public IActionResult AddUser([FromBody] CreateUserDto userDto)
    {
        User user = _mapper.Map<User>(userDto);
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }

    /// <summary>
    /// Pesquisa uma quantidade de usuários no banco de dados. Valor padrão: 20 itens.
    /// </summary>
    /// <param name="skip">Número do index da página</param>
    /// <param name="take">Número de elementos a serem carregados por página</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso pesquisa seja feita com sucesso</response>
    [HttpGet]
    public IEnumerable<ReadUserDto> GetAllUser([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
        return _mapper.Map<List<ReadUserDto>>(_dbContext.Users.Skip(skip).Take(take));
    }

    /// <summary>
    /// Pesquisa um usuário no banco de dados
    /// </summary>
    /// <param name="id">Inteiro com o Id para encontrar o usuário</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso pesquisa seja feita com sucesso</response>
    [HttpGet("{id}")]
    public IActionResult GetUserById(int id)
    {
        var user = _dbContext.Users.FirstOrDefault(user => user.Id == id);
        if (user == null) return NotFound();
        var userDto = _mapper.Map<ReadGameDto>(user);
        return Ok(userDto);
    }

    /// <summary>
    /// Edita um usuário no banco de dados.
    /// </summary>
    /// <param name="id">Inteiro com o Id para encontrar o usuário</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso pesquisa seja feita com sucesso</response>
    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] UpdateUserDto userDto)
    {
        var user = _dbContext.Users.FirstOrDefault(user => user.Id == id);
        if (user == null) return NotFound();
        _mapper.Map(userDto, user);
        _dbContext.SaveChanges();
        return NoContent();

    }

    /// <summary>
    /// Edita um dado de um usuário no banco de dados.
    /// </summary>
    /// <param name="id">Inteiro com o Id para encontrar o usuário</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso pesquisa seja feita com sucesso</response>
    [HttpPatch("{id}")]
    public IActionResult UpdateUserPatch(int id, JsonPatchDocument<UpdateUserDto> patch)
    {
        var user = _dbContext.Users.FirstOrDefault(user => user.Id == id);
        if (user == null) return NotFound();

        var UserToUpdate = _mapper.Map<UpdateUserDto>(user);

        patch.ApplyTo(UserToUpdate, ModelState);

        if (!TryValidateModel(UserToUpdate))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(UserToUpdate, user);
        _dbContext.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Deleta um usuário no banco de dados.
    /// </summary>
    /// <param name="id">Inteiro com o Id para encontrar o usuário</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso pesquisa seja feita com sucesso</response>
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var user = _dbContext.Users.FirstOrDefault(user => user.Id == id);
        if (user == null) return NotFound();
        _dbContext.Remove(user);
        _dbContext.SaveChanges();
        return NoContent();
    }
}
