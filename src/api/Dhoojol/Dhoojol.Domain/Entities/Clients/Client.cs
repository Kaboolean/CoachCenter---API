using Dhoojol.Domain.Entities.Base;
using Dhoojol.Domain.Entities.Coaches;
using Dhoojol.Domain.Entities.Sessions;
using Dhoojol.Domain.Entities.Users;

namespace Dhoojol.Domain.Entities.Clients
{
    public class Client : Entity
    {
        public string? Goal { get; set; }
        public int? Age { get; set; }
        public int? Height { get; set; }
        public int? Weight { get; set; }
        public string? Handicap { get; set; }
        public User User { get; set; } = null!;
        public Coach? Coach { get; set; }
        public List<SessionParticipant> Participations { get; set; } = new();
    }
}
