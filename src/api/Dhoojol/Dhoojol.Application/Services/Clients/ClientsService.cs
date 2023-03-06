using Dhoojol.Application.Models.Clients;
using Dhoojol.Domain.Entities.Clients;
using Dhoojol.Domain.Entities.Users;
using Dhoojol.Infrastructure.EfCore.Repositories.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhoojol.Application.Services.Clients
{
    internal class ClientsService : IClientsService
    {
        private readonly IClientRepository _clientRepository;

        public ClientsService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public async Task<GetClientModel> GetClient(Guid id)
        {
            var query = await _clientRepository.GetClientByUserId(id);
            var client = new GetClientModel { 
                Goal = query?.Goal, 
                Age = query?.Age, 
                Height = query?.Height,
                Weight = query?.Weight, 
                Handicap = query?.Handicap};
            return client;
        }
        public async Task CreateAsync(User user)
        {
            var client = new Client { User = user };
            await _clientRepository.CreateAsync(client);
        }

    }
}
