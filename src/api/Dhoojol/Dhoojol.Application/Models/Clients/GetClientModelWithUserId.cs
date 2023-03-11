using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhoojol.Application.Models.Clients
{
    public class GetClientModelWithUserId : GetClientModel
    {
        public Guid userId { get; set; }
    }
}
