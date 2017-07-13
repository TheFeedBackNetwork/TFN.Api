using System.Collections.Generic;
using TFN.Domain.Models.Entities;

namespace TFN.Infrastructure.Repositories.TransientUserAccountAggregate.InMemory
{
    public static class InMemoryTransientUsers
    {
        public static List<TransientUserAccount> TransientUsers = new List<TransientUserAccount>();
    }
}