using Dhoojol.Domain.Entities.Base;

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
    public string? CompanyName { get; set; }
    public string UserType { get; set; } = null!;
}

