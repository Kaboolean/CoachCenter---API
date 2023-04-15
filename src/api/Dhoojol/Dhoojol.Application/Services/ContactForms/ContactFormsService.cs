using Dhoojol.Application.Models.ContactForms;
using Dhoojol.Application.Models.Users;
using Dhoojol.Application.Services.Auth;
using Dhoojol.Domain.Entities.ContactForms;
using Dhoojol.Domain.Entities.Users;
using Dhoojol.Infrastructure.EfCore.Repositories.ContactForms;
using Dhoojol.Infrastructure.EfCore.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace Dhoojol.Application.Services.ContactForms
{
    internal class ContactFormsService : IContactFormsService
    {
        private readonly IContactFormRepository _contactFormRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        public ContactFormsService (IContactFormRepository contactFormRepository, IUserRepository userRepository, IAuthService authService)
        {
            _contactFormRepository = contactFormRepository;
            _userRepository = userRepository;
            _authService = authService;
        }
        public async Task<List<GetContactFormModel>> GetAllContactForms()
        {
            var query = _contactFormRepository.AsQueryable().Where(e=>e.FirstName != null).Select(e => new GetContactFormModel
            {
                Id = e.Id,
                CreatedDate = e.CreatedDate,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                TextRequest = e.TextRequest,

            });
            var query2 = _contactFormRepository.AsQueryable().Where(e => e.User != null).Select(e => new GetContactFormModel
            {
                Id = e.Id,
                User = new GetUserModel
                {
                    Id = e.User.Id,
                    UserName = e.User.UserName,
                    Email = e.User.Email,
                    LastLoginDate = e.User.LastLoginDate,
                    FirstName = e.User.FirstName,
                    LastName = e.User.LastName,
                    BirthDate = e.User.BirthDate,
                    UserType = e.User.UserType

                },
                TextRequest = e.TextRequest,
            });
            var result = await query.ToListAsync();
            var result2 = await query2.ToListAsync();
            result.AddRange(result2);
            return result;
        }
        public async Task<Guid> CreateUserContactForm(CreateContactFormModel model)
        {
                var userId = _authService.GetUserId();
                User user = await _userRepository.GetAsync(userId);
                ContactForm contactForm = new ContactForm
                {
                    User = user,
                    TextRequest = model.TextRequest,
                };
                await _contactFormRepository.CreateAsync(contactForm);
                return contactForm.Id;
        }
        public async Task<Guid> CreateGuestContactForm(CreateContactFormModel model)
        {
                ContactForm contactForm = new ContactForm
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    TextRequest = model.TextRequest,
                };
                await _contactFormRepository.CreateAsync(contactForm);
                return contactForm.Id;
        }
    }
}
