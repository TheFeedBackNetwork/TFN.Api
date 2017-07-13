using System;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using TFN.Domain.Architecture.Models;
using TFN.Domain.Models.ValueObjects;
using TFN.Domain.Models.Extensions;

namespace TFN.Domain.Models.Entities
{
    public class UserAccount : DomainEntity<Guid> , IAggregateRoot
    {
        public string Username { get; private set; }
        public string NormalizedUsername { get; private set; }
        public string ProfilePictureUrl { get; private set; } 
        public string Email { get; private set; }
        public string NormalizedEmail { get; private set; }
        public string FullName { get; private set; }
        public string HashedPassword { get; private set; }
        public string ChangePasswordKey { get; private set; }
        public Biography Biography { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }
        public bool IsActive { get; private set; }

        public UserAccount(string username,string hashedPassword, string profilePictureUrl, string email, string name, Biography biography)
            : this(Guid.NewGuid(), username, username.ToUpperInvariant(),hashedPassword, String.Empty,profilePictureUrl, email, email.ToUpperInvariant(), name, biography,DateTime.UtcNow,DateTime.UtcNow, true)
        {

        }
        private UserAccount(Guid id, string username, string normalizedUsername, string hashedPassword, string changePasswordKey, string profilePictureUrl, string email, string normalizedEmail, string fullName, Biography biography, DateTime created, DateTime modified, bool isActive)
            : base(id)
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException($"The username [{nameof(username)}] is either null or empty.");
            }
            if(username.Length < 3)
            {
                throw new ArgumentException($"The username [{nameof(username)}] is too short.");
            }
            if (username.Length > 16)
            {
                throw new ArgumentException($"The username [{nameof(username)}] is too long.");
            }
            if (!email.IsEmail())
            {
                throw new ArgumentException($"The email [{nameof(email)}] is not a valid email.");
            }
            if (email.ToUpperInvariant() != normalizedEmail)
            {
                throw new ArgumentException($"The email [{nameof(email)}] does not have a valid normalized email.");
            }
            if (username.ToUpperInvariant() != normalizedUsername)
            {
                throw new ArgumentException($"The username [{nameof(username)}] does not have a valid normalizied username.");
            }
            if (created > modified)
            {
                throw new ArgumentException($"created date: [{nameof(created)}] cannot be later than modified date: [{nameof(modified)}].");
            }

            Username = username;
            NormalizedUsername = normalizedUsername;
            Email = email;
            NormalizedEmail = normalizedEmail;
            ProfilePictureUrl = profilePictureUrl;
            FullName = fullName;
            Biography = biography;
            HashedPassword = hashedPassword;
            ChangePasswordKey = changePasswordKey;
            Created = created;
            IsActive = isActive;
        }

        public static UserAccount Hydrate(Guid id, string username, string normalizedUsername,string hashedPassword, string changePasswordKey, string profilePictureUrl, string email, string normalizedEmail, string name, Biography biography, DateTime created, DateTime modified, bool isActive)
        {
            return new UserAccount(id, username,normalizedUsername, hashedPassword,changePasswordKey,profilePictureUrl, email, normalizedEmail, name, biography, created, modified, isActive);
        }

        public IReadOnlyList<Claim> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, Id.ToString()),
                new Claim(JwtClaimTypes.PreferredUserName, Username),
                new Claim(JwtClaimTypes.Email, Email),
                new Claim(JwtClaimTypes.EmailVerified, Email),
                new Claim(JwtClaimTypes.IdentityProvider, "idsvr"),
                //new Claim(JwtClaimTypes.FullName, user.Username) wanna get rid of shit

            };

            if (!string.IsNullOrWhiteSpace(FullName))
            {
                claims.Add(new Claim(JwtClaimTypes.Name, FullName));
            }

            if (!string.IsNullOrWhiteSpace(ProfilePictureUrl))
            {
                claims.Add(new Claim(JwtClaimTypes.Picture, ProfilePictureUrl));
            }

            return claims;
        }

        public void UpdateHashedPassword(string hashedPassword)
        {
            HashedPassword = hashedPassword;
            ChangePasswordKey = String.Empty;
        }

        public void UpdateChangePasswordKey(string changePasswordKey)
        {
            ChangePasswordKey = changePasswordKey;
        }
    }
}
