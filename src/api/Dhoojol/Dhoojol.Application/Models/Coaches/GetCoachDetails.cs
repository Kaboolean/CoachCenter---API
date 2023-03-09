using Dhoojol.Application.Models.Users;

namespace Dhoojol.Application.Models.Coaches
{
    public class GetCoachDetails : GetUserModel
    {
        public string? Grades { get; set; }
        public string? Description { get; set; }
        public int HourlyRate { get; set; }
    }
}
