using System.ComponentModel.DataAnnotations;

namespace MyRPGList.Data.Dtos;

public class ReadGameDto
{
    public string Name { get; set; }
    public string Developer { get; set; }
    public string? Description { get; set; }
    public DateTime SearchTime { get; set; } = DateTime.Now;
}
