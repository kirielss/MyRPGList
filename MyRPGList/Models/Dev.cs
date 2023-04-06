using System.ComponentModel.DataAnnotations;

namespace MyRPGList.Models
{
    public class Dev
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo de nome é obrigatório.")]
        public string Name { get; set; }
        public ICollection<Game> Games { get; set; }
        public string? Description { get; set; }
    }
}
