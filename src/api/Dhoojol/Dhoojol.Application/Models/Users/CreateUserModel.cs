namespace Dhoojol.Application.Models.Users;

public class CreateUserModel
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Password { get; set; } = null!;
}

