using Dhoojol.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhoojol.Domain.Entities.Coaches
{
    public class Coach : Entity
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public DateTime? LastLoginDate { get; set; }
        public string? FirstName { get; set; } 
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
    }
}
