using Dhoojol.Application.Models.Clients;
using Dhoojol.Domain.Entities.Users;

namespace Dhoojol.Application.Services.Clients
{
    public interface IClientsService
    {
        Task<GetClientModel> GetClientByUserId(Guid id);
        Task CreateAsync(User user);
        Task UpdateClient(UpdateClientModel model);
        Task DeleteClientAsync(Guid userId);
    }
}
