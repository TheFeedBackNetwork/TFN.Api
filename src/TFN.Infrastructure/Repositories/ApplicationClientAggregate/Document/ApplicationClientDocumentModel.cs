using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TFN.Infrastructure.Architecture.Documents;
using TFN.Infrastructure.Architecture.Documents.Attributes;
using TFN.Infrastructure.Architecture.Documents.Models;

namespace TFN.Infrastructure.Repositories.ApplicationClientAggregate.Document
{
    [CollectionOptions("configuration","applicationClient")]
    public sealed class ApplicationClientDocumentModel : ApplicationClientDocumentModel<Guid>
    {
        public ApplicationClientDocumentModel(Guid id, DateTime created, DateTime modified)
            : base(id, created, modified)
        {

        }

        public ApplicationClientDocumentModel() { }
    }

    public class ApplicationClientDocumentModel<TKey> : BaseDocument<TKey>
    {
        public ApplicationClientDocumentModel() { }

        [JsonProperty(PropertyName = "clientId")]
        public string ClientId { get; set; }

        [JsonProperty(PropertyName = "clientName")]
        public string ClientName { get; set; }

        [JsonProperty(PropertyName = "clientUri")]
        public string ClientUri { get; set; }

        [JsonProperty(PropertyName = "logoUri")]
        public string LogoUri { get; set; }

        [JsonProperty(PropertyName = "logoutUri")]
        public string LogoutUri { get; set; }

        [JsonProperty(PropertyName = "protocolType")]
        public string ProtocolType { get; set; } = IdentityServer4.IdentityServerConstants.ProtocolTypes.OpenIdConnect;

        [JsonProperty(PropertyName = "enabled")]
        public bool Enabled { get; set; } = true;

        [JsonProperty(PropertyName = "requireConsent")]
        public bool RequireConsent { get; set; } = false;

        [JsonProperty(PropertyName = "requireClientSecret")]
        public bool RequireClientSecret { get; set; }

        [JsonProperty(PropertyName = "requirePkce")]
        public bool RequirePkce { get; set; }

        [JsonProperty(PropertyName = "allowPlainTextPkce")]
        public bool AllowPlainTextPkce { get; set; }

        [JsonProperty(PropertyName = "allowAccessTokenViaBrowser")]
        public bool AllowAccessTokensViaBrowser { get; set; } = true;

        [JsonProperty(PropertyName = "logoutSessionRequired")]
        public bool LogoutSessionRequired { get; set; } = true;

        [JsonProperty(PropertyName = "allowRememberConsent")]
        public bool AllowRememberConsent { get; set; } = true;

        [JsonProperty(PropertyName = "allowOfflineAccess")]
        public bool AllowOfflineAccess { get; set; }

        [JsonProperty(PropertyName = "updateAccessTokenClaimsOnRefresh")]
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }

        [JsonProperty(PropertyName = "enableLocalLogin")]
        public bool EnableLocalLogin { get; set; }

        [JsonProperty(PropertyName = "includeJwtId")]
        public bool IncludeJwtId { get; set; } = true;

        [JsonProperty(PropertyName = "alwaysSendClientClaims")]
        public bool AlwaysSendClientClaims { get; set; } = true;

        [JsonProperty(PropertyName = "prefixClientClaims")]
        public bool PrefixClientClaims { get; set; } = true;

        [JsonProperty(PropertyName = "identityTokenLifetime")]
        public int IdentityTokenLifetime { get; set; } = 300;

        [JsonProperty(PropertyName = "accessTokenLifetime")]
        public int AccessTokenLifetime { get; set; } = 3600;

        [JsonProperty(PropertyName = "authorizationCodeLifetime")]
        public int AuthorizationCodeLifetime { get; set; } = 300;

        [JsonProperty(PropertyName = "absoluteRefreshTokenLifetime")]
        public int AbsoluteRefreshTokenLifetime { get; set; } = 2592000;

        [JsonProperty(PropertyName = "slidingRefreshTokenLifetime")]
        public int SlidingRefreshTokenLifetime { get; set; } = 1296000;

        [JsonProperty(PropertyName = "refreshTokenUsage")]
        public TokenUsageDocumentEnum RefreshTokenUsage { get; set; } = TokenUsageDocumentEnum.OneTimeOnly;

        [JsonProperty(PropertyName = "refreshTokenExpiration")]
        public TokenExpirationDocumentEnum RefreshTokenExpiration { get; set; } = TokenExpirationDocumentEnum.Absolute;

        [JsonProperty(PropertyName = "accessTokenType")]
        public AccessTokenTypeDocumentEnum AccessTokenType { get; set; } = AccessTokenTypeDocumentEnum.Jwt;

        [JsonProperty(PropertyName = "allowedCorsOrigins")]
        public ICollection<string> AllowedCorsOrigins { get; set; }

        [JsonProperty(PropertyName = "allowedGrantTypes")]
        public ICollection<string> AllowedGrantTypes { get; set; } = new List<string>();

        [JsonProperty(PropertyName = "redirectUris")]
        public ICollection<string> RedirectUris { get; set; }

        [JsonProperty(PropertyName = "postLogoutRedirectUris")]
        public ICollection<string> PostLogoutRedirectUris { get; set; }

        [JsonProperty(PropertyName = "allowedScopes")]
        public ICollection<string> AllowedScopes { get; set; } = new List<string>();

        [JsonProperty(PropertyName = "identityProviderRestrictions")]
        public ICollection<string> IdentityProviderRestrictions { get; set; }

        [JsonProperty(PropertyName = "clientSecrets")]
        public ICollection<SecretDocumentModel> ClientSecrets { get; set; } = new List<SecretDocumentModel>();

        [JsonProperty(PropertyName = "claims")]
        public ICollection<ClientClaimDocumentModel> Claims { get; set; } = new List<ClientClaimDocumentModel>();

        public ApplicationClientDocumentModel(TKey id, DateTime created, DateTime modified)
            : base(id, "applicationClient", created, modified)
        {

        }
    }

    public class ClientClaimDocumentModel
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }

    public enum TokenUsageDocumentEnum
    {
        ReUse = 0,
        OneTimeOnly = 1
    }

    public enum TokenExpirationDocumentEnum
    {
        Sliding = 0,
        Absolute = 1
    }

    public enum AccessTokenTypeDocumentEnum
    {
        Jwt = 0,
        Reference = 1,
    }

}