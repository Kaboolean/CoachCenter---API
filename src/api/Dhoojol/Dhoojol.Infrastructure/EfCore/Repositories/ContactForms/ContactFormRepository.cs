using Dhoojol.Domain.Entities.ContactForms;
using Dhoojol.Infrastructure.EfCore.Repositories.Base;


namespace Dhoojol.Infrastructure.EfCore.Repositories.ContactForms
{
    public interface IContactFormRepository : IRepository<ContactForm>
    {

    }
    internal class ContactFormRepository : EfRepository<ContactForm>, IContactFormRepository
    {
        private readonly DhoojolContext _dbContext;

        public ContactFormRepository(DhoojolContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
