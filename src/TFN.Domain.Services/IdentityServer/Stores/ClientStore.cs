using System;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Extensions.Logging;
using TFN.Domain.Interfaces.Repositories;

namespace TFN.Domain.Services.IdentityServer.Stores
{
    public class ClientStore : IClientStore
    {
        public IApplicationClientRepository ClientRepository { get; private set; }
        public ILogger Logger { get; private set; }
        public ClientStore(IApplicationClientRepository clientRepository, ILogger<ClientStore> logger)
        {
            ClientRepository = clientRepository;
            Logger = logger;
        }

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var applicationClient = await ClientRepository.Find(clientId);

            if (applicationClient == null)
            {
                return null;
            }

            return applicationClient.Client;
        }
    }
}