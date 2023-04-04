namespace MyRPGList.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Dev Developer { get; set; }
        public string? Description { get; set; }
    }
}
