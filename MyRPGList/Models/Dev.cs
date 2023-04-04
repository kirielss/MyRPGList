namespace MyRPGList.Models
{
    public class Dev
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Game> Games { get; set; }
        public string? Description { get; set; }
    }
}
