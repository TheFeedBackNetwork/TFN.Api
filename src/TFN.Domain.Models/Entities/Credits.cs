using System;
using TFN.Domain.Architecture.Models;

namespace TFN.Domain.Models.Entities
{
    public class Credits : DomainEntity<Guid> , IAggregateRoot
    {
        public Guid UserId { get; private set; }
        public string Username { get; private set; }
        public string NormalizedUsername { get; private set; }
        public int TotalCredits { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }

        private Credits(Guid id, Guid userId, string username,string normalizedUsername, int totalCredits,DateTime created, DateTime modified, bool isActive)
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
            if (totalCredits < 0)
            {
                throw new ArgumentException($"The total credits [{nameof(totalCredits)}] can not be negative.");
            }

            if (username.ToUpperInvariant() != normalizedUsername)
            {
                throw new ArgumentException($"The username [{nameof(username)}] does not have a valid normalizied username.");
            }

            UserId = userId;
            Username = username;
            NormalizedUsername = normalizedUsername;
            TotalCredits = totalCredits;
            IsActive = isActive;
        }

        public Credits(Guid userId, string userName)
            : this(Guid.NewGuid(),userId,userName, userName.ToUpperInvariant(), 10,DateTime.UtcNow,DateTime.UtcNow, true)
        { }

        public static Credits Hydrate(Guid id, Guid userId, string username, string normalizedUsername, int totalCredits,DateTime created, DateTime modified, bool isActive)
        {
            return new Credits(id,userId,username, normalizedUsername,totalCredits,created, modified, isActive);
        }

        public Credits ChangeTotalCredits(int amount)
        {
            var newCredits = TotalCredits + amount;
            if (newCredits < 0)
            {
                throw new InvalidOperationException("Credits will result in a negative score");
            }
            return Hydrate(Id,UserId,Username,NormalizedUsername,newCredits,Created,Modified, IsActive);
        }
    }
}