namespace Dhoojol.Application.Models.Sessions
{
    public class CreateSessionModel
    {
        public string Name { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Location { get; set; } = null!;
        public int Duration { get; set; }
        public string Description { get; set; } = null!;
        public List<string> Tags { get; set; } = null!;
    }
}
