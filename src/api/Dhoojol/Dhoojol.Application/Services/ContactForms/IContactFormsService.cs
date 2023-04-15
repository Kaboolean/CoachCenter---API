using Dhoojol.Application.Models.ContactForms;

namespace Dhoojol.Application.Services.ContactForms
{
    public interface IContactFormsService
    {
        Task<List<GetContactFormModel>> GetAllContactForms();

        Task<Guid> CreateUserContactForm(CreateContactFormModel model);
        Task<Guid> CreateGuestContactForm(CreateContactFormModel model);
    }
}
