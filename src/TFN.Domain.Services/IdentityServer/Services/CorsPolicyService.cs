using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Services;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Extensions;

namespace TFN.Domain.Services.IdentityServer.Services
{
    public class CorsPolicyService : ICorsPolicyService
    {
        private IClientRepository ClientRepository { get; set; }

        public CorsPolicyService(IClientRepository clientRepository)
        {
            ClientRepository = clientRepository;
        }
        public async Task<bool> IsOriginAllowedAsync(string origin)
        {
            var urls = await ClientRepository.FindAllAllowedCorsOrigins();

            var origins = urls.Select(x => x.GetOrigin());

            var result = origins.Contains(origin, StringComparer.OrdinalIgnoreCase);

            return result;
        }
    }
}
