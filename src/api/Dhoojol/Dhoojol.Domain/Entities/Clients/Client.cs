using Dhoojol.Domain.Entities.Base;
using Dhoojol.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhoojol.Domain.Entities.Clients
{
    public class Client : Entity
    {
        public string? Goal { get; set; }
        public int? Age { get; set; }
        public int? Height { get; set; }
        public int? Weight { get; set; }
        public string? Handicap { get; set; }
        public User User { get; set; } = null!;
    }
}
