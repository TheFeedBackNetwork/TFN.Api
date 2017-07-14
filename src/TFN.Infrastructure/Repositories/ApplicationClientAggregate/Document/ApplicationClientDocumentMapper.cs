using System;
using System.Linq;
using System.Security.Claims;
using IdentityServer4.Models;
using TFN.Domain.Models.Entities.IdentityServer;
using TFN.Infrastructure.Architecture.Documents.Models;
using TFN.Infrastructure.Architecture.Mapping;

namespace TFN.Infrastructure.Repositories.ApplicationClientAggregate.Document
{
    public class ApplicationClientDocumentMapper : IAggregateMapper<ApplicationClient, ApplicationClientDocumentModel, Guid>
    {
        public ApplicationClient CreateFrom(ApplicationClientDocumentModel dataEntity)
        {
            return ApplicationClient.Hydrate(
                dataEntity.Id,
                dataEntity.Created,
                dataEntity.Modified,
                CreatePartialFrom(dataEntity));
        }

        public ApplicationClientDocumentModel CreateFrom(ApplicationClient domainEntity)
        {
            var client = domainEntity.Client;

            return new ApplicationClientDocumentModel(domainEntity.Id, domainEntity.Created, domainEntity.Modified)
            {
                ClientId = client.ClientId,
                ClientName = client.ClientName,
                ClientUri = client.ClientUri,
                LogoUri = client.LogoUri,
                LogoutUri = client.LogoutUri,
                ProtocolType = client.ProtocolType,
                Enabled = client.Enabled,
                RequireConsent = client.RequireConsent,
                RequireClientSecret = client.RequireClientSecret,
                RequirePkce = client.RequirePkce,
                AllowPlainTextPkce = client.AllowPlainTextPkce,
                AllowAccessTokensViaBrowser = client.AllowAccessTokensViaBrowser,
                LogoutSessionRequired = client.LogoutSessionRequired,
                AllowRememberConsent = client.AllowRememberConsent,
                AllowOfflineAccess = client.AllowOfflineAccess,
                UpdateAccessTokenClaimsOnRefresh = client.UpdateAccessTokenClaimsOnRefresh,
                EnableLocalLogin = client.EnableLocalLogin,
                IncludeJwtId = client.IncludeJwtId,
                AlwaysSendClientClaims = client.AlwaysSendClientClaims,
                PrefixClientClaims = client.PrefixClientClaims,
                IdentityTokenLifetime = client.IdentityTokenLifetime,
                AccessTokenLifetime = client.AccessTokenLifetime,
                AuthorizationCodeLifetime = client.AuthorizationCodeLifetime,
                AbsoluteRefreshTokenLifetime = client.AbsoluteRefreshTokenLifetime,
                SlidingRefreshTokenLifetime = client.SlidingRefreshTokenLifetime,
                RefreshTokenUsage = (TokenUsageDocumentEnum)Enum.Parse(typeof(TokenUsageDocumentEnum), client.RefreshTokenUsage.ToString()),
                RefreshTokenExpiration = (TokenExpirationDocumentEnum)Enum.Parse(typeof(TokenExpirationDocumentEnum), client.RefreshTokenExpiration.ToString()),
                AccessTokenType = (AccessTokenTypeDocumentEnum)Enum.Parse(typeof(AccessTokenTypeDocumentEnum), client.AccessTokenType.ToString()),
                AllowedCorsOrigins = client.AllowedCorsOrigins,
                AllowedGrantTypes = client.AllowedGrantTypes.ToList(),
                RedirectUris = client.RedirectUris,
                PostLogoutRedirectUris = client.PostLogoutRedirectUris,
                AllowedScopes = client.AllowedScopes,
                IdentityProviderRestrictions = client.IdentityProviderRestrictions,
                ClientSecrets = client.ClientSecrets.Select(x => new SecretDocumentModel
                {
                    Description = x.Description,
                    Expiration = x.Expiration,
                    Type = x.Type,
                    Value = x.Value
                }).ToList(),
                Claims = client.Claims.Select(x => new ClientClaimDocumentModel
                {
                    Type = x.Type,
                    Value = x.Value
                }).ToList()
            };
        }

        private Client CreatePartialFrom(ApplicationClientDocumentModel client)
        {
            return new Client
            {
                ClientId = client.ClientId,
                ClientName = client.ClientName,
                ClientUri = client.ClientUri,
                LogoUri = client.LogoUri,
                LogoutUri = client.LogoutUri,
                ProtocolType = client.ProtocolType,
                Enabled = client.Enabled,
                RequireConsent = client.RequireConsent,
                RequireClientSecret = client.RequireClientSecret,
                RequirePkce = client.RequirePkce,
                AllowPlainTextPkce = client.AllowPlainTextPkce,
                AllowAccessTokensViaBrowser = client.AllowAccessTokensViaBrowser,
                LogoutSessionRequired = client.LogoutSessionRequired,
                AllowRememberConsent = client.AllowRememberConsent,
                AllowOfflineAccess = client.AllowOfflineAccess,
                UpdateAccessTokenClaimsOnRefresh = client.UpdateAccessTokenClaimsOnRefresh,
                EnableLocalLogin = client.EnableLocalLogin,
                IncludeJwtId = client.IncludeJwtId,
                AlwaysSendClientClaims = client.AlwaysSendClientClaims,
                PrefixClientClaims = client.PrefixClientClaims,
                IdentityTokenLifetime = client.IdentityTokenLifetime,
                AccessTokenLifetime = client.AccessTokenLifetime,
                AuthorizationCodeLifetime = client.AuthorizationCodeLifetime,
                AbsoluteRefreshTokenLifetime = client.AbsoluteRefreshTokenLifetime,
                SlidingRefreshTokenLifetime = client.SlidingRefreshTokenLifetime,
                RefreshTokenUsage = (TokenUsage)Enum.Parse(typeof(TokenUsage), client.RefreshTokenUsage.ToString()),
                RefreshTokenExpiration = (TokenExpiration)Enum.Parse(typeof(TokenExpiration), client.RefreshTokenExpiration.ToString()),
                AccessTokenType = (AccessTokenType)Enum.Parse(typeof(AccessTokenType), client.AccessTokenType.ToString()),
                AllowedCorsOrigins = client.AllowedCorsOrigins,
                AllowedGrantTypes = client.AllowedGrantTypes,
                RedirectUris = client.RedirectUris,
                PostLogoutRedirectUris = client.PostLogoutRedirectUris,
                AllowedScopes = client.AllowedScopes,
                IdentityProviderRestrictions = client.IdentityProviderRestrictions,
                ClientSecrets = client.ClientSecrets?.Select(x => new Secret(x.Value, x.Description, x.Expiration)).ToList(),
                Claims = client.Claims?.Select(x => new Claim(x.Type, x.Value)).ToList()
            };
        }
    }
}