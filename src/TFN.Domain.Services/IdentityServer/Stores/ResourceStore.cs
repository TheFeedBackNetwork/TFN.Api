using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using TFN.Domain.Interfaces.Repositories;

namespace TFN.Domain.Services.IdentityServer.Stores
{
    public class ResourceStore : IResourceStore
    {
        public IUserIdentityResourceRepository ProductIdentityResourceRepository { get; private set; }
        public IProductApiResourceRepository ProductApiResourceRepository { get; private set; }

        public ResourceStore(IProductApiResourceRepository productApiResourceRepository,
            IUserIdentityResourceRepository productIdentityResourceRepository)
        {
            ProductApiResourceRepository = productApiResourceRepository;
            ProductIdentityResourceRepository = productIdentityResourceRepository;
        }

        public async Task<ApiResource> FindApiResourceAsync(string name)
        {
            var productApiResource = await ProductApiResourceRepository.Find(name);

            return productApiResource.ApiResource;
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var productApiResources = await ProductApiResourceRepository.FindAll(scopeNames.ToList());

            var apiResources = productApiResources.Select(x => x.ApiResource);

            return apiResources;
        }

        public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var productIdentityResources = await ProductIdentityResourceRepository.FindAll(scopeNames.ToList());

            var identityResources = productIdentityResources.Select(x => x.IdentityResource);

            return identityResources;
        }

        public async Task<Resources> GetAllResourcesAsync()
        {
            var productIdentityResources = await ProductIdentityResourceRepository.FindAll();
            var productApiResources = await ProductApiResourceRepository.FindAll();

            var identityResources = productIdentityResources.Select(x => x.IdentityResource);
            var apiResources = productApiResources.Select(x => x.ApiResource);

            var resources = new Resources(identityResources, apiResources);

            return resources;
        }
    }
}