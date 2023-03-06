using Dhoojol.Domain.Entities.Clients;
using Dhoojol.Domain.Entities.Users;
using Dhoojol.Infrastructure.EfCore.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhoojol.Infrastructure.EfCore.Repositories.Clients;

public interface IClientRepository : IRepository<Client>
{
    Task<Client> GetClientByUserId(Guid id);
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
}
