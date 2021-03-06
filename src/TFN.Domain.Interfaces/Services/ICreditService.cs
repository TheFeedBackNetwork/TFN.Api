﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TFN.Domain.Models.Entities;

namespace TFN.Domain.Interfaces.Services
{
    public interface ICreditService
    {
        Task AwardCredits(Guid fromUser, Guid toUser, int amount);
        Task ReduceCredits(Credits credits, int amount);
        Task<IReadOnlyList<Credits>> FindLeaderBoard(string continuationToken);
        Task<Credits> Find(Guid id);
        Task<Credits> FindByUserId(Guid userId);
        Task<Credits> FindByUsername(string username);
        Task Add(Credits credits);
        Task<IReadOnlyList<Credits>> SearchUsers(string searchToken, string continuationToken);
    }
}