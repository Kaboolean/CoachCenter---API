using Dhoojol.Domain.Entities.Base;
using Dhoojol.Domain.Entities.Clients;
using Dhoojol.Domain.Entities.Coaches;

namespace Dhoojol.Domain.Entities.Users;

public class User : Entity
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime? LastLoginDate { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? BirthDate { get; set; }
    public string Password { get; set; } = null!;
    public string UserType { get; set; } = null!;
    public Coach? Coach { get; set; }
    public Client? Client { get; set; }

}



