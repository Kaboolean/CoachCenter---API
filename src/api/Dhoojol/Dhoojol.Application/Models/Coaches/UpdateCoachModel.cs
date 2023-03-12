using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhoojol.Application.Models.Coaches
{
    public class UpdateCoachModel
    {
        public Guid UserId { get; set; }
        public string? Grades { get; set; }
        public string? Description { get; set; }
        public int HourlyRate { get; set; }
    }
}
