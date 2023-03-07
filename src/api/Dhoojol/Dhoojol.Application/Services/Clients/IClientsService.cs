using Dhoojol.Application.Models.Clients;
using Dhoojol.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhoojol.Application.Services.Clients
{
    public interface IClientsService
    {
        Task CreateAsync(User user);
        Task<GetClientModel> GetClient(Guid id);
        Task DeleteClientAsync(Guid userId);
    }
}
