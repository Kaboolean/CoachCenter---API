using Dhoojol.Application.Models.Clients;
using Dhoojol.Domain.Entities.Clients;
using Dhoojol.Domain.Entities.Coaches;
using Dhoojol.Domain.Entities.Users;
using Dhoojol.Infrastructure.EfCore.Repositories.Clients;
using Microsoft.EntityFrameworkCore;

namespace Dhoojol.Application.Services.Clients
{
    internal class ClientsService : IClientsService
    {
        private readonly IClientRepository _clientRepository;

        public ClientsService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public async Task<GetClientModel> GetClientByUserId(Guid id)
        {
            var query = _clientRepository.AsQueryable().Select(e=>
            new GetClientModel { 
                Goal = e.Goal, 
                Age = e.Age, 
                Height = e.Height,
                Weight = e.Weight, 
                Handicap = e.Handicap,
                UserId = e.User.Id,
                UserName = e.User.UserName,
                Email = e.User.Email,
                FirstName = e.User.FirstName,
                LastName = e.User.LastName,
            });
            var client = await query.FirstOrDefaultAsync(e=> e.UserId == id);
            if (client == null)
            {
                throw new Exception("Client not found");
            }
            return client;
        }

        public async Task CreateAsync(User user)
        {
            var client = new Client { User = user };
            await _clientRepository.CreateAsync(client);
        }

        public async Task UpdateClient(UpdateClientModel model)
        {
            var client = await _clientRepository.GetClientByUserId(model.UserId);
            client.Goal = model.Goal;
            client.Height = model.Height;
            client.Weight = model.Weight;
            await _clientRepository.UpdateAsync(client);
        }

        public async Task DeleteClientAsync(Guid userId)
        {
            var clientId = await _clientRepository.GetClientIdByUserId(userId);
            if (clientId != Guid.Empty)
            {
                await _clientRepository.DeleteAsync(clientId);
            }
        }
    }
}
