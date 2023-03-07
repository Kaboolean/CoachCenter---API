using Dhoojol.Domain.Entities.Coaches;
using Dhoojol.Infrastructure.EfCore.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhoojol.Infrastructure.EfCore.Repositories.Coaches;

public interface ICoachRepository : IRepository<Coach>
{
    Task<Guid> GetCoachIdByUserId(Guid id);
}

internal class CoachRepository : EfRepository<Coach>, ICoachRepository
{
    private readonly DhoojolContext _dbContext;

    public CoachRepository(DhoojolContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> GetCoachIdByUserId(Guid userId)
    {
        var query = await _dbContext.Coaches.AsQueryable().FirstOrDefaultAsync(c => c.User.Id == userId);
        if(query == null)
        {
            return Guid.Empty;
        }
        return query.Id;
    }
}
