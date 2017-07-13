using System;
using System.Linq;
using System.Threading.Tasks;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;

namespace TFN.Infrastructure.Repositories.TransientUserAccountAggregate.InMemory
{
    public class TransientUserAccountInMemoryRepository : ITransientUserAccountRepository
    {
        public Task Add(TransientUserAccount entity)
        {
            InMemoryTransientUsers.TransientUsers.Add(entity);
            return Task.CompletedTask;
        }

        public Task Delete(Guid id)
        {
            InMemoryTransientUsers.TransientUsers.RemoveAll(x => x.Id == id);
            return Task.CompletedTask;
        }

        public Task<TransientUserAccount> Find(Guid id)
        {
            return Task.FromResult(InMemoryTransientUsers.TransientUsers.SingleOrDefault(x => x.Id == id));
        }

        public Task<TransientUserAccount> FindByEmail(string email)
        {
            return Task.FromResult(InMemoryTransientUsers.TransientUsers.SingleOrDefault(x => x.Email == email));
        }

        public Task<TransientUserAccount> FindByVerificationKey(string emailVerificationKey)
        {
            return Task.FromResult(InMemoryTransientUsers.TransientUsers.SingleOrDefault(x => x.VerificationKey == emailVerificationKey));
        }

        public Task Update(TransientUserAccount entity)
        {
            Delete(entity.Id);
            Add(entity);
            return Task.CompletedTask;
        }

        public Task<TransientUserAccount> FindByUsername(string username)
        {
            return Task.FromResult(InMemoryTransientUsers.TransientUsers.SingleOrDefault(x => x.Username == username));
        }

        public Task<bool> Any()
        {
            throw new NotImplementedException();
        }
    }
}