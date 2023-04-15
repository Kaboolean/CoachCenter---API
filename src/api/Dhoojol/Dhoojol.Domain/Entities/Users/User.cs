using Dhoojol.Domain.Entities.Base;
using Dhoojol.Domain.Entities.Clients;
using Dhoojol.Domain.Entities.Coaches;
using System.ComponentModel.DataAnnotations;

namespace Dhoojol.Domain.Entities.Users;

public class User : Entity
{
    [Required(AllowEmptyStrings = false)]
    [StringLength(25)]
    public string UserName { get; set; } = null!;

    [StringLength(50)]
    public string Email { get; set; } = null!;

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public DateTime? LastLoginDate { get; set; }

    [StringLength(25)]
    public string? FirstName { get; set; }

    [StringLength(25)]
    public string? LastName { get; set; }
    public DateTime? BirthDate { get; set; }

    [Required]
    public string Password { get; set; } = null!;

    [Required]
    public string UserType { get; set; } = null!;

    public Coach? Coach { get; set; }
    public Client? Client { get; set; }
}



