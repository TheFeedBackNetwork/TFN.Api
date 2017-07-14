using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;

namespace TFN.Infrastructure.Repositories.CreditsAggregate.InMemory
{
    public class CreditInMemoryRepository //: ICreditRepository
    {
        /*public Task Add(Credits entity)
        {
            InMemoryCredits.Credits.Add(entity);
            return Task.CompletedTask;
        }

        public Task<Credits> Find(Guid id)
        {
            return Task.FromResult(InMemoryCredits.Credits.SingleOrDefault(x => x.Id == id));
        }

        public Task Update(Credits entity)
        {
            InMemoryCredits.Credits.RemoveAll(x => x.Id == entity.Id);
            InMemoryCredits.Credits.Add(entity);
            return Task.CompletedTask;
        }

        public Task<Credits> FindByUsername(string username)
        {
            return Task.FromResult(InMemoryCredits.Credits.SingleOrDefault(x => x.Username == username));
        }

        public Task<Credits> FindByUserId(Guid userId)
        {
            return Task.FromResult(InMemoryCredits.Credits.SingleOrDefault(x => x.UserId == userId));
        }

        public Task<IReadOnlyList<Credits>> FindHighestCredits(string continuationToken)
        {
            IReadOnlyList<Credits> leaders =
                InMemoryCredits.Credits.OrderBy(x => x.TotalCredits).Skip(continuationToken).Take(limit).ToList();

            return Task.FromResult(leaders);
        }
        public Task<IReadOnlyList<Credits>> FindUsers(string searchToken, string continuationToken)
        {
            IReadOnlyList<Credits> credits =
                InMemoryCredits.Credits.Where(x => x.Username.StartsWith(searchToken)).Skip(continuationToken).Take(limit).ToList();
            return Task.FromResult(credits);
        }

        public Task<bool> Any()
        {
            throw new NotImplementedException();
        }*/
    }
}