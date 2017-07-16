using System.Collections.Generic;
using System.Linq;
using IdentityModel;
using IdentityServer4.Models;
using TFN.Domain.Models.Entities.IdentityServer;

namespace TFN.StaticData
{
    public static class ProductApiResources
    {
        private static IEnumerable<ProductApiResource> GetResources()
        {
            var coreResources = new List<ApiResource>
            {
                new ApiResource
                {
                    Description = "TFN Core resources",
                    DisplayName = "TFN Core",
                    Enabled = true,
                    UserClaims = new List<string>
                    {
                        JwtClaimTypes.Email,
                        JwtClaimTypes.EmailVerified,
                        JwtClaimTypes.PreferredUserName,
                        JwtClaimTypes.Picture,
                        JwtClaimTypes.Name,
                        "biography",
                    },
                    Scopes = new List<Scope>
                    {
                        new Scope
                        {
                            Name = "posts.read"
                        },
                        new Scope
                        {
                            Name = "posts.write"
                        },
                        new Scope
                        {
                            Name = "posts.edit"
                        },
                        new Scope
                        {
                            Name = "posts.delete"
                        },
                        new Scope
                        {
                            Name = "credits.read"
                        },
                        new Scope
                        {
                            Name = "credits.edit"
                        },
                        new Scope
                        {
                            Name = "credits.delete"
                        },
                        new Scope
                        {
                            Name = "posts.write"
                        },
                        new Scope
                        {
                            Name = "ip.read"
                        },
                        new Scope
                        {
                            Name = "users.read"
                        },
                    }
                }
            };

            var tracksResource = new List<ApiResource>
            {
                new ApiResource
                {
                    Description = "TFN Tracks resources",
                    DisplayName = "TFN Tracks API",
                    Enabled = true,
                    UserClaims = new List<string>
                    {
                        JwtClaimTypes.Email,
                        JwtClaimTypes.EmailVerified,
                        JwtClaimTypes.PreferredUserName,
                        JwtClaimTypes.Picture,
                        JwtClaimTypes.Name,
                        "biography",
                    },
                    Scopes = new List<Scope>
                    {
                        new Scope
                        {
                            Name = "tracks.read"
                        },
                        new Scope
                        {
                            Name = "tracks.write"
                        },
                        new Scope
                        {
                            Name = "tracks.delete"
                        },
                    }
                }
            };

            var all = new List<ApiResource>();
            all.AddRange(coreResources);
            all.AddRange(tracksResource);

            var productApiResources = all.Select(x => new ProductApiResource(x));

            return productApiResources;
        }

        public static IEnumerable<ProductApiResource> ApiResourceses => GetResources();
    }
}