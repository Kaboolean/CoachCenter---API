using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhoojol.Application.Models.Coaches
{
    public class GetCoachModelWithUserId : GetCoachModel
    {
        public Guid userId { get; set; }
    }
}
