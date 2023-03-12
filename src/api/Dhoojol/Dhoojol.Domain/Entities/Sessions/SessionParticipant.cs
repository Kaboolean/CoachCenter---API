using Dhoojol.Domain.Entities.Base;
using Dhoojol.Domain.Entities.Clients;

namespace Dhoojol.Domain.Entities.Sessions
{
    public class SessionParticipant : Entity
    {
        public Guid SessionId { get; set; }
        public Guid ClientId { get; set; }
        public Session Session { get; set; } = null!;
        public Client Client { get; set; } = null!;

    }
}
