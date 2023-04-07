using System.ComponentModel.DataAnnotations;

namespace MyRPGList.Models
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public ICollection<Game> UserGames { get; set; }
    }
}
