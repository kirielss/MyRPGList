using MyRPGList.Models;
using System.ComponentModel.DataAnnotations;

namespace MyRPGList.Data.Dtos;

public class CreateDevDto
{

    [Required(ErrorMessage = "O campo de nome é obrigatório")]
    public string Name { get; set; }
    public string? Description { get; set; } 
}
