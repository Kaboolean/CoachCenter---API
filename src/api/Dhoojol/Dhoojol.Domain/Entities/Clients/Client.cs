using Dhoojol.Domain.Entities.Base;
using Dhoojol.Domain.Entities.Coaches;
using Dhoojol.Domain.Entities.Sessions;
using Dhoojol.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace Dhoojol.Domain.Entities.Clients
{
    public class Client : Entity
    {
        public User User { get; set; } = null!;
        public Guid UserId { get; set; }
        [MaxLength(250)]
        public string? Goal { get; set; }

        [MaxLength(100)]
        public int? Age { get; set; }
        [MaxLength(250)]
        public int? Height { get; set; }
        [MaxLength(500)]
        public int? Weight { get; set; }
        [MaxLength(100)]
        public string? Handicap { get; set; }
        public List<SessionParticipant> Participations { get; set; } = new();
        public Coach? Coach { get; set; }
    }
}
