using Dhoojol.Domain.Entities.Base;
using Dhoojol.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace Dhoojol.Domain.Entities.ContactForms
{
    public class ContactForm : Entity
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [StringLength(25)]
        public string? FirstName { get; set; }

        [StringLength(25)]
        public string? LastName { get; set; }

        [StringLength(50)]
        public string? Email { get; set; }
        public User? User { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(1000)]
        public string TextRequest { get; set; } = null!;
    }
}
