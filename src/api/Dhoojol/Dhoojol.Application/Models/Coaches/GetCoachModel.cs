
namespace Dhoojol.Application.Models.Coaches
{
    public class GetCoachModel
    {
        public string? Grades { get; set; }
        public string? Description { get; set; }
        public int HourlyRate { get; set; }

        public Guid UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set;} = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
