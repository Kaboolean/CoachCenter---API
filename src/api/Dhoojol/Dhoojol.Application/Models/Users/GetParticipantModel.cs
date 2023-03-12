using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhoojol.Application.Models.Users
{
    public class GetParticipantModel
    {
        public int? Age { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
