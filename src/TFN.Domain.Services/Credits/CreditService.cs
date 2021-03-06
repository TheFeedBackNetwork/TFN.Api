﻿using System;
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

        public async Task AwardCredits(Guid fromUserId, Guid toUserId, int amount)
        {
            var credits = await CreditRepository.FindByUserId(toUserId);
            if (credits == null)
            {
                throw new ArgumentException($"{nameof(credits)} cannot be null.");
            }

            var newCredits = credits.ChangeTotalCredits(amount);
            await CreditRepository.Update(newCredits);
        }

        public async Task ReduceCredits(Models.Entities.Credits credits, int amount)
        {
            var newCredits = credits.ChangeTotalCredits(-amount);
            await CreditRepository.Update(newCredits);
        }

        public async Task<IReadOnlyList<Models.Entities.Credits>> FindLeaderBoard(string continuationToken)
        {
            return await CreditRepository.FindHighestCredits(continuationToken);
        }

        public async Task<Models.Entities.Credits> Find(Guid id)
        {
            return await CreditRepository.Find(id);
        }

        public async Task<Models.Entities.Credits> FindByUserId(Guid userId)
        {
            return await CreditRepository.FindByUserId(userId);
        }

        public async Task<Models.Entities.Credits> FindByUsername(string username)
        {
            return await CreditRepository.FindByUsername(username);
        }

        public async Task Add(Models.Entities.Credits credits)
        {
            await CreditRepository.Add(credits);
        }

        public async Task<IReadOnlyList<Models.Entities.Credits>> SearchUsers(string searchToken, string continuationToken)
        {
            return await CreditRepository.FindUsers(searchToken, continuationToken);
        }
    }
}