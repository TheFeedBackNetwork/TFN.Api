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
        Task<IReadOnlyList<Credits>> FindLeaderBoard(short offset, short limit);
        Task<Credits> GetAsync(Guid id);
        Task<Credits> GetByUserIdAsync(Guid userId);
        Task<Credits> GetByUsernameAsync(string username);
        Task AddAsync(Credits credits);
        Task<IReadOnlyList<Credits>> SearchUsers(string searchToken, int offset, int limit);
    }
}