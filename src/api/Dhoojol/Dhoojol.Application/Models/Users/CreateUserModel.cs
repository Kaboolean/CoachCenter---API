namespace Dhoojol.Application.Models.Users;

public class CreateUserModel
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string UserType { get; set; } = null!;
}

