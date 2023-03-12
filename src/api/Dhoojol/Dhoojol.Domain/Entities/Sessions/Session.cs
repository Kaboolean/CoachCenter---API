using Dhoojol.Domain.Entities.Base;
using Dhoojol.Domain.Entities.Coaches;


namespace Dhoojol.Domain.Entities.Sessions
{
    public class Session : Entity
    {
        public string Name { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Location { get; set; } = null!;
        public int Duration { get; set; }
        public string Description { get; set; } = null!;
        public Coach Coach { get; set; } = null!;
        public List<SessionParticipant> Participants { get; set; } = new();
        public List<string> Tags { get; set; } = new();
    }
}
