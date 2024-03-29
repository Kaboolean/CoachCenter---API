﻿using Dhoojol.Domain.Entities.Coaches;
using Dhoojol.Domain.Entities.Users;
using Dhoojol.Infrastructure.EfCore.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Dhoojol.Infrastructure.EfCore.Repositories.Coaches;

public interface ICoachRepository : IRepository<Coach>
{
    Task<Guid> GetCoachIdByUserId(Guid id);
    Task<Coach> GetCoachByUserId(Guid id);
}

internal class CoachRepository : EfRepository<Coach>, ICoachRepository
{
    private readonly DhoojolContext _dbContext;

    public CoachRepository(DhoojolContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Coach> GetCoachByUserId(Guid id)
    {
        var query = await _dbContext.Coaches.AsQueryable().FirstOrDefaultAsync(c => c.User.Id == id);
        if(query == null )
        {
            throw new Exception("Coach not found");
        }
        return query;
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
