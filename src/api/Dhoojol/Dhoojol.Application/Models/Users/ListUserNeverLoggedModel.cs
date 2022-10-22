namespace Dhoojol.Application.Models.Users;

public class ListUserNeverLoggedModel
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime? LastLoginDate { get; set; }
}
