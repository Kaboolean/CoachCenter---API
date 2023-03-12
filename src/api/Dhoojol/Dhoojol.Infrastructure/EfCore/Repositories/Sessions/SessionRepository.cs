using Dhoojol.Domain.Entities.Sessions;
using Dhoojol.Infrastructure.EfCore.Repositories.Base;


namespace Dhoojol.Infrastructure.EfCore.Repositories.Sessions;

public interface ISessionRepository : IRepository<Session>
{

}

internal class SessionRepository : EfRepository<Session>, ISessionRepository
{
    private readonly DhoojolContext _dbContext;

    public SessionRepository(DhoojolContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
