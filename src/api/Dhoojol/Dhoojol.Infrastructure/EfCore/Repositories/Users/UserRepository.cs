using Dhoojol.Domain.Entities.Users;
using Dhoojol.Infrastructure.EfCore.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Dhoojol.Infrastructure.EfCore.Repositories.Users;

public interface IUserRepository : IRepository<User>
{
    Task<List<User>> GetNeverLoggedAsync(CancellationToken token = default);
    Task<string> GetUserTypeById(Guid id);
}

internal class UserRepository : EfRepository<User>, IUserRepository
{
    private readonly DhoojolContext _dbContext;

    public UserRepository(DhoojolContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<User>> GetNeverLoggedAsync(CancellationToken token = default)
    {
        var query = _dbContext.Users
            .AsNoTracking()
            .Where(u => u.LastLoginDate == null);

        return query.ToListAsync(token);
    }
    public async Task<string> GetUserTypeById(Guid id)
    {
        var query = await _dbContext.Users.FirstOrDefaultAsync(u=>u.Id == id);
        return query!.UserType;
    }
}

