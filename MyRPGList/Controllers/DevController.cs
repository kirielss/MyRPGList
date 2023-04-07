using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MyRPGList.Data;
using MyRPGList.Data.Dtos;
using MyRPGList.Data.DTOs;
using MyRPGList.Models;

namespace MyRPGList.Controllers;

[ApiController]
[Route("[controller]")]
public class DevController : Controller
{
    private MyRpgListDbContext _dbContext;
    private IMapper _mapper;

    public DevController(MyRpgListDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um desenvolvedor ao banco de dados
    /// </summary>
    /// <param name="devDto">Objeto com os campos necessários para criação de um desenvolvedor</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    public IActionResult AddDev([FromBody] CreateDevDto devDto)
    {
        Dev dev = _mapper.Map<Dev>(devDto);
        _dbContext.Developers.Add(dev);
        _dbContext.SaveChanges();
        return CreatedAtAction(nameof(GetDevById), new { id = dev.Id }, dev);
    }

    /// <summary>
    /// Pesquisa uma quantidade de desenvolvedores no banco de dados. Valor padrão: 20 itens.
    /// </summary>
    /// <param name="skip">Número do index da página</param>
    /// <param name="take">Número de elementos a serem carregados por página</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso pesquisa seja feita com sucesso</response>
    [HttpGet]
    public IEnumerable<ReadDevDto> GetAllDevs([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
        return _mapper.Map<List<ReadDevDto>>(_dbContext.Developers.Skip(skip).Take(take));
    }

    /// <summary>
    /// Pesquisa um desenvolvedor no banco de dados
    /// </summary>
    /// <param name="id">Inteiro com o Id para encontrar o desenvolvedor</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso pesquisa seja feita com sucesso</response>
    [HttpGet("{id}")]
    public IActionResult GetDevById(int id)
    {
        var dev = _dbContext.Developers.FirstOrDefault(dev => dev.Id == id);
        if (dev == null) return NotFound();
        var devDto = _mapper.Map<ReadGameDto>(dev);
        return Ok(devDto);
    }

    /// <summary>
    /// Edita um desenvolvedor no banco de dados.
    /// </summary>
    /// <param name="id">Inteiro com o Id para encontrar o desenvolvedor</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso pesquisa seja feita com sucesso</response>
    [HttpPut("{id}")]
    public IActionResult UpdateDev(int id, [FromBody] UpdateDevDto devDto)
    {
        var dev = _dbContext.Developers.FirstOrDefault(dev => dev.Id == id);
        if (dev == null) return NotFound();
        _mapper.Map(devDto, dev);
        _dbContext.SaveChanges();
        return NoContent();

    }

    /// <summary>
    /// Edita um dado de um desenvolvedor no banco de dados.
    /// </summary>
    /// <param name="id">Inteiro com o Id para encontrar o Desenvolvedor</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso pesquisa seja feita com sucesso</response>
    [HttpPatch("{id}")]
    public IActionResult UpdateDevPatch(int id, JsonPatchDocument<UpdateDevDto> patch)
    {
        var dev = _dbContext.Developers.FirstOrDefault(dev => dev.Id == id);
        if (dev == null) return NotFound();

        var DevToUpdate = _mapper.Map<UpdateDevDto>(dev);

        patch.ApplyTo(DevToUpdate, ModelState);

        if (!TryValidateModel(DevToUpdate))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(DevToUpdate, dev);
        _dbContext.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Deleta um desenvolvedor no banco de dados.
    /// </summary>
    /// <param name="id">Inteiro com o Id para encontrar o desenvolvedor</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso pesquisa seja feita com sucesso</response>
    [HttpDelete("{id}")]
    public IActionResult DeleteDev(int id)
    {
        var dev = _dbContext.Developers.FirstOrDefault(dev => dev.Id == id);
        if (dev == null) return NotFound();
        _dbContext.Remove(dev);
        _dbContext.SaveChanges();
        return NoContent();
    }



}