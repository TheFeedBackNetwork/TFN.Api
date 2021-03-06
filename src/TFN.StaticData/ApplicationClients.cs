﻿using System.Collections.Generic;
using System.Linq;
using IdentityServer4.Models;
using TFN.Domain.Models.Entities.IdentityServer;

namespace TFN.StaticData
{
    public static class ApplicationClients
    {
        private static IEnumerable<ApplicationClient> GetClients()
        {
            var clients = new List<Client>
            {
                new Client
                {
                    ClientId = "tfn_postman",
                    ClientName = "TFN Postman Client",
                    IncludeJwtId = true,
                    ClientClaimsPrefix = "client_",
                    AccessTokenType = AccessTokenType.Jwt,
                    AlwaysSendClientClaims = true,
                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "profile",
                        "biography",
                        "profile_picture_url",
                        "posts.write",
                        "posts.read",
                        "posts.edit",
                        "posts.delete",
                        "tracks.read",
                        "tracks.write",
                        "tracks.delete",
                        "tracks.edit",
                        "credits.read",
                        "credits.edit",
                        "credits.delete",
                        "users.read",
                        "ip.read",
                        "offline_access"
                    }
                },
                new Client
                {
                    ClientId = "tfn_frontend",
                    ClientName = "TFN Frontent Client",
                    RequireConsent =  false,
                    IncludeJwtId = true,
                    ClientClaimsPrefix = "client_",
                    AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenLifetime = 3600,
                    AllowAccessTokensViaBrowser = true,
                    AlwaysSendClientClaims = true,
                    AllowedGrantTypes = GrantTypes.ImplicitAndClientCredentials,
                    RequireClientSecret = false,
                    RedirectUris = { "http://localhost:5001/oidc-callback","http://localhost:5001/oidc-renew","http://localhost:5000/oidc-callback","http://localhost:5000/oidc-renew"  },
                    AllowedCorsOrigins = {"http://localhost:5000"},
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "profile",
                        "biography",
                        "profile_picture_url",
                        "posts.write",
                        "posts.read",
                        "posts.edit",
                        "posts.delete",
                        "tracks.read",
                        "tracks.write",
                        "tracks.delete",
                        "tracks.edit",
                        "credits.read",
                        "credits.write",
                        "credits.delete",
                        "ip.read",
                        "users.read"
                    }
                }
            };

            var applictionClients = clients.Select(x => new ApplicationClient(x));

            return applictionClients;
        }

        public static IEnumerable<ApplicationClient> Clients => GetClients();
    }
}