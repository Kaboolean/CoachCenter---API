using Dhoojol.Domain.Entities.Sessions;


namespace Dhoojol.Application.Models.Sessions
{
    public class ListSessionModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Location { get; set; } = null!;
        public int Duration { get; set; }
        public string Description { get; set; } = null!;
        public Guid CoachId { get; set; }
        public string? CoachFirstName { get; set; }
        public string? CoachLastName { get; set; }
        public List<string> Tags { get; set; }
        public int ParticipantCount { get; set; }
    }
}
