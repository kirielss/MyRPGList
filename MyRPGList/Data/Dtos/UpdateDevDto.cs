using System.ComponentModel.DataAnnotations;

namespace MyRPGList.Data.Dtos
{
    public class UpdateDevDto
    {
        [Required(ErrorMessage = "O campo de nome é obrigatório")]
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
