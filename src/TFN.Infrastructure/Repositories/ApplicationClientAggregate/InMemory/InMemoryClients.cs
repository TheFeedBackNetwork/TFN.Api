﻿using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace TFN.Infrastructure.Repositories.ApplicationClientAggregate.InMemory
{
    public class InMemoryClients
    {
        public static IEnumerable<Client> Clients = new List<Client>
        {
            new Client
            {
                ClientId = "tfn_postman",
                ClientName = "TFN Postman Client",
                IncludeJwtId = true,
                PrefixClientClaims = true,
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
                    "credits.read",
                    "credits.write",
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
                PrefixClientClaims = true,
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
                    "credits.read",
                    "credits.write",
                    "credits.delete",
                    "ip.read",
                    "users.read"
                }
            },
            new Client
            {
                ClientId = "collectorapp_collectorapi",
                ClientName = "MVC Client",
                AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                RequireConsent = false,
                ClientSecrets =
                {
                    new Secret("BF2360DB1EBF4A3317F68482DAA34E91AF81A968AEBCB56F4245C278C78B1D4F".Sha256())
                },

                AccessTokenType = AccessTokenType.Reference,
                RedirectUris = { "app://collector.whereismytransport.com/end" },
                PostLogoutRedirectUris = { "app://collector.whereismytransport.com/end" },

                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    "biography",
                    "profile_picture_url",
                    "posts.write",
                    "posts.read",
                    "posts.edit",
                    "posts.delete",
                    "tracks.read",
                    "tracks.write",
                    "tracks.delete",
                    "credits.read",
                    "credits.write",
                    "credits.delete",
                }
            }
        };
    }
}