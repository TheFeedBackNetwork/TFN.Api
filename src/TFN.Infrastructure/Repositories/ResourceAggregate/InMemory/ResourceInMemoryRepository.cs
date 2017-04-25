﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using TFN.Domain.Interfaces.Repositories;

namespace TFN.Infrastructure.Repositories.ResourceAggregate.InMemory
{
    public class ResourceInMemoryRepository : IResourceRepository
    {
        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            var api = from a in InMemoryResources.ApiResources
                      where a.Name == name
                      select a;

            return Task.FromResult(api.FirstOrDefault());
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            if (scopeNames == null) throw new ArgumentNullException(nameof(scopeNames));

            var api = from a in InMemoryResources.ApiResources
                      from s in a.Scopes
                      where scopeNames.Contains(s.Name)
                      select a;

            return Task.FromResult(api);
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            if (scopeNames == null) throw new ArgumentNullException(nameof(scopeNames));

            var identity = from i in InMemoryResources.IdentityResources
                           where scopeNames.Contains(i.Name)
                           select i;

            return Task.FromResult(identity);
        }

        public Task<Resources> GetAllResources()
        {
            var result = new Resources(InMemoryResources.IdentityResources, InMemoryResources.ApiResources);
            return Task.FromResult(result);
        }
    }
}