using System.Collections.Generic;
using System.Linq;
using IdentityServer4.Models;
using TFN.Domain.Models.Entities.IdentityServer;

namespace TFN.StaticData
{
    public static class UserIdentityResources
    {
        private static IEnumerable<UserIdentityResource> GetResources()
        {
            var identityResources = new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "biography",
                    Description = "Non standard identity scope for holding biography information",
                    UserClaims = new HashSet<string> {"profile_picture_url"}
                }
            };

            var resources = identityResources.Select(x => new UserIdentityResource(x));

            return resources;
        }

        public static IEnumerable<UserIdentityResource> IdentityResources => GetResources();
    }
}