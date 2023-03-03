using Dhoojol.Domain.Entities.Users;
using Dhoojol.Infrastructure.EfCore.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhoojol.Infrastructure.EfCore.Repositories.Auth;

public interface IAuthRepository : IRepository<User>
{
    Task<User> GetUserByUsername(string userName);
}

internal class AuthRepository : EfRepository<User>, IAuthRepository
{
    private readonly DhoojolContext _dbContext;

    public AuthRepository(DhoojolContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    public Task<User> GetUserByUsername(string userName)
    {
        var query = _dbContext.Users.AsQueryable().FirstOrDefault(u => u.UserName == userName);
        if (query is not null)
        {
            return Task.FromResult(query);
        }
        else
        {
            throw new Exception($"User not found");
        }
    }

}