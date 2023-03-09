using Dhoojol.Domain.Entities.Clients;
using Dhoojol.Infrastructure.EfCore.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Dhoojol.Infrastructure.EfCore.Repositories.Clients;

public interface IClientRepository : IRepository<Client>
{
    Task<Client> GetClientByUserId(Guid id);
    Task<Guid> GetClientIdByUserId(Guid userId);
}

internal class ClientRepository : EfRepository<Client>, IClientRepository
{
    private readonly DhoojolContext _dbContext;

    public ClientRepository(DhoojolContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Client> GetClientByUserId(Guid id)
    {
        var query = await _dbContext.Clients.AsQueryable().FirstOrDefaultAsync(c => c.User.Id == id);
        if (query is not null)
        {
            return query;
        }
        else
        {
            throw new Exception($"Client not found");
        }
    }

    public async Task<Guid> GetClientIdByUserId(Guid userId)
    {
        var query = await _dbContext.Clients.AsQueryable().FirstOrDefaultAsync(c=>c.User.Id == userId);
        if (query == null)
        {
            return Guid.Empty;
        }
        return query.Id;
    }
}
