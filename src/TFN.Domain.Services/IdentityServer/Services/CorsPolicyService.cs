using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Services;
using TFN.Domain.Interfaces.Repositories;

namespace TFN.Domain.Services.IdentityServer.Services
{
    public class CorsPolicyService : ICorsPolicyService
    {
        public IApplicationClientRepository ApplicationClientRepository { get; private set; }

        public CorsPolicyService(IApplicationClientRepository applicationClientRepository)
        {
            ApplicationClientRepository = applicationClientRepository;
        }

        public async Task<bool> IsOriginAllowedAsync(string origin)
        {

            var urls = await ApplicationClientRepository.FindAllAllowedCorsOrigins();

            var origins = urls.Select(GetOrigin);

            var result = origins.Contains(origin, StringComparer.OrdinalIgnoreCase);

            return result;
        }

        private static string GetOrigin(string url)
        {
            if (url != null && (url.StartsWith("http://") || url.StartsWith("https://")))
            {
                var idx = url.IndexOf("//", StringComparison.Ordinal);
                if (idx > 0)
                {
                    idx = url.IndexOf("/", idx + 2, StringComparison.Ordinal);
                    if (idx >= 0)
                    {
                        url = url.Substring(0, idx);
                    }
                    return url;
                }
            }

            return null;
        }
    }
}
