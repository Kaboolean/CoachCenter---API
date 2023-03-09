
namespace Dhoojol.Application.Models.Coaches
{
    public class GetCoachModel
    {
        public Guid CoachId { get; set; }
        public string? Grades { get; set; }
        public string? Description { get; set; }
        public int HourlyRate { get; set; }
    }
}
