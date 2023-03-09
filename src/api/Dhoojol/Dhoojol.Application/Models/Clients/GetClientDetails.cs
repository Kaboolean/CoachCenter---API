using Dhoojol.Application.Models.Users;

namespace Dhoojol.Application.Models.Clients
{
    public class GetClientDetails : GetUserModel
    {
        public string? Goal { get; set; }
        public int? Age { get; set; }
        public int? Height { get; set; }
        public int? Weight { get; set; }
        public string? Handicap { get; set; }
    }
}
