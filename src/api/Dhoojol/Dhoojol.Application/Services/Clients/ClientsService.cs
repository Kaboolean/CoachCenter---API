﻿using Dhoojol.Application.Models.Clients;
using Dhoojol.Domain.Entities.Clients;
using Dhoojol.Domain.Entities.Users;
using Dhoojol.Infrastructure.EfCore.Repositories.Clients;


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
            var query = await _clientRepository.GetClientByUserId(id);
            var client = new GetClientModel { 
                Goal = query?.Goal, 
                Age = query?.Age, 
                Height = query?.Height,
                Weight = query?.Weight, 
                Handicap = query?.Handicap,
            };
            return client;
        }

        public async Task CreateAsync(User user)
        {
            var client = new Client { User = user };
            await _clientRepository.CreateAsync(client);
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
