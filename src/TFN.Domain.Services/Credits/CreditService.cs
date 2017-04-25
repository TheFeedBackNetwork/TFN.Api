using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Interfaces.Services;

namespace TFN.Domain.Services.Credits
{
    public class CreditService : ICreditService
    {
        public ICreditRepository CreditRepository { get; private set; }
        public CreditService(ICreditRepository creditRepository)
        {
            CreditRepository = creditRepository;
        }

        public async Task AwardCreditAsync(Guid fromUserId, Guid toUserId, int amount)
        {
            var credits = await CreditRepository.GetByUserId(toUserId);
            if (credits == null)
            {
                throw new ArgumentException($"{nameof(credits)} cannot be null.");
            }

            var newCredits = credits.ChangeTotalCredits(amount);
            await CreditRepository.UpdateAsync(newCredits);
        }

        public async Task ReduceCreditsAsync(Models.Entities.Credits credits, int amount)
        {
            var newCredits = credits.ChangeTotalCredits(-amount);
            await CreditRepository.UpdateAsync(newCredits);
        }

        public async Task<IReadOnlyList<Models.Entities.Credits>> GetLeaderBoardAsync(short offset, short limit)
        {
            return await CreditRepository.GetHighestCredits(offset, limit);
        }

        public async Task<Models.Entities.Credits> GetAsync(Guid id)
        {
            return await CreditRepository.GetAsync(id);
        }

        public async Task<Models.Entities.Credits> GetByUserIdAsync(Guid userId)
        {
            return await CreditRepository.GetByUserId(userId);
        }

        public async Task<Models.Entities.Credits> GetByUsernameAsync(string username)
        {
            return await CreditRepository.GetByUsername(username);
        }

        public async Task AddAsync(Models.Entities.Credits credits)
        {
            await CreditRepository.AddAsync(credits);
        }

        public async Task<IReadOnlyList<Models.Entities.Credits>> SearchUsers(string searchToken, int offset, int limit)
        {
            return await CreditRepository.SearchUsers(searchToken, offset, limit);
        }
    }
}