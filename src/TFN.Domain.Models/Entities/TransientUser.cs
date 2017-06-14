using System;
using TFN.Domain.Architecture.Models;
using TFN.Domain.Models.Extensions;

namespace TFN.Domain.Models.Entities
{
    public class TransientUserAccount : DomainEntity<Guid>, IAggregateRoot
    {
        public string Username { get; private set; }
        public string NormalizedUsername { get; private set; }
        public string Email { get; private set; }
        public string NormalizedEmail { get; private set; }
        public string VerificationKey { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }

        public TransientUserAccount(string username, string email, string emailVerificationKey)
            : this(Guid.NewGuid(), username, username.ToUpperInvariant(), email, email.ToUpperInvariant(), emailVerificationKey, DateTime.UtcNow, DateTime.UtcNow)
        {
            
        }

        private TransientUserAccount(Guid id, string username, string normalizedUsername, string email, string normalizedEmail, string verificationKey, DateTime created, DateTime modified)
            : base(id)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException($"The username [{nameof(username)}] is either null or empty.");
            }
            if (username.Length < 3)
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
            if (string.IsNullOrWhiteSpace(verificationKey))
            {
                throw new ArgumentNullException($"{nameof(verificationKey)} may not be empty or whitespace.");
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
            NormalizedUsername = NormalizedUsername;
            Email = email;
            NormalizedEmail = normalizedEmail;
            VerificationKey = verificationKey;
            Created = created;
            Modified = modified;
        }

        public static TransientUserAccount Hydrate(Guid id, string username, string normalizedUsername, string email, string normalizedEmail, string emailVerificationKey, DateTime created, DateTime modified)
        {
            return new TransientUserAccount(id,username,normalizedUsername,email, normalizedEmail, emailVerificationKey,created ,modified);
        }
    }
}