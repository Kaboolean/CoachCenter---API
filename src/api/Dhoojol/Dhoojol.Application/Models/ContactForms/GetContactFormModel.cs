using Dhoojol.Application.Models.Users;

namespace Dhoojol.Application.Models.ContactForms
{
    public class GetContactFormModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public GetUserModel? User { get; set; }
        public string TextRequest { get; set; } = null!;
    }
}
