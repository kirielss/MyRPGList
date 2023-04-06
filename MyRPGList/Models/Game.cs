using System.ComponentModel.DataAnnotations;

namespace MyRPGList.Models;

public class Game
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required(ErrorMessage = "Please enter the name of the game")]
    [MaxLength(100, ErrorMessage ="The name of the game cannot exceed 100 characters")]
    public string Name { get; set; }
    [Required(ErrorMessage ="Please enter the name of the developer")]
    [MaxLength(100, ErrorMessage = "The name of the developer cannot exceed 100 characters")]
    public virtual Dev Developer { get; set; }
    public string? Description { get; set; }
}
