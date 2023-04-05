using System.ComponentModel.DataAnnotations;

namespace MyRPGList.Data.DTOs
{
    public class UpdateGameDto
    {
        [Required(ErrorMessage = "Please enter the name of the game")]
        [StringLength(100, ErrorMessage = "The name of the game cannot exceed 100 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter the name of the developer")]
        [StringLength(100, ErrorMessage = "The name of the developer cannot exceed 100 characters")]
        public string Developer { get; set; }
        public string? Description { get; set; }
    }
}
