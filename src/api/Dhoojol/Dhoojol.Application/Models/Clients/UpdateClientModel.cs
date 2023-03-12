using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhoojol.Application.Models.Clients
{
    public class UpdateClientModel
    {
        public Guid UserId { get; set; }
        public string? Goal { get; set; }
        public int? Age { get; set; }
        public int? Height { get; set; }
        public int? Weight { get; set; }
        public string? Handicap { get; set; }
    }
}
