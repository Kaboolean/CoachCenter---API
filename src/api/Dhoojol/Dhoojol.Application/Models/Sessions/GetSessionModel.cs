using Dhoojol.Application.Models.Coaches;

namespace Dhoojol.Application.Models.Sessions
{
    public class GetSessionModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Location { get; set; } = null!;
        public int Duration { get; set; }
        public string Description { get; set; } = null!;
        public GetCoachModel Coach { get; set; } = null!;
        public List<string> Tags { get; set; }
    }
}
