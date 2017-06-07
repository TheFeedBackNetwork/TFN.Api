using System;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using TFN.Domain.Architecture.Models;
using TFN.Domain.Models.ValueObjects;
using TFN.Domain.Models.Extensions;

namespace TFN.Domain.Models.Entities
{
    public class User : DomainEntity<Guid> , IAggregateRoot
    {
        public string Username { get; private set; }
        public string NormalizedUsername { get; private set; }
        public string ProfilePictureUrl { get; private set; } 
        public string Email { get; private set; }
        public string NormalizedEmail { get; private set; }
        public string Name { get; private set; }
        public Biography Biography { get; private set; }
        public DateTime Created { get; private set; }
        public bool IsActive { get; private set; }

        public User(string username, string profilePictureUrl, string email, string name, Biography biography)
            : this(Guid.NewGuid(), username, username.ToUpperInvariant(),profilePictureUrl, email, email.ToUpperInvariant(), name, biography,DateTime.UtcNow, true)
        {

        }
        private User(Guid id, string username, string normalizedUsername, string profilePictureUrl, string email, string normalizedEmail, string name, Biography biography, DateTime created, bool isActive)
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

            Username = username;
            NormalizedUsername = normalizedUsername;
            Email = email;
            NormalizedEmail = normalizedEmail;
            ProfilePictureUrl = profilePictureUrl;
            Name = name;
            Biography = biography;
            Created = created;
            IsActive = isActive;
        }

        public static User Hydrate(Guid id, string username, string normalizedUsername, string profilePictureUrl, string email, string normalizedEmail, string name, Biography biography, DateTime created, bool isActive)
        {
            return new User(id, username,normalizedUsername,profilePictureUrl, email, normalizedEmail, name, biography, created, isActive);
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
                //new Claim(JwtClaimTypes.Name, user.Username) wanna get rid of shit

            };

            if (!string.IsNullOrWhiteSpace(Name))
            {
                claims.Add(new Claim(JwtClaimTypes.Name, Name));
            }

            if (!string.IsNullOrWhiteSpace(ProfilePictureUrl))
            {
                claims.Add(new Claim(JwtClaimTypes.Picture, ProfilePictureUrl));
            }

            return claims;
        }
    }
}
