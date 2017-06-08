using System;
using System.Linq;
using System.Threading.Tasks;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;

namespace TFN.Infrastructure.Repositories.TransientUserAggregate.InMemory
{
    public class TransientUserInMemoryRepository : ITransientUserRepository
    {
        public Task Add(TransientUser entity)
        {
            InMemoryTransientUsers.TransientUsers.Add(entity);
            return Task.CompletedTask;
        }

        public Task Delete(Guid id)
        {
            InMemoryTransientUsers.TransientUsers.RemoveAll(x => x.Id == id);
            return Task.CompletedTask;
        }

        public Task<TransientUser> Find(Guid id)
        {
            return Task.FromResult(InMemoryTransientUsers.TransientUsers.SingleOrDefault(x => x.Id == id));
        }

        public Task<TransientUser> GetByEmailAsync(string email)
        {
            return Task.FromResult(InMemoryTransientUsers.TransientUsers.SingleOrDefault(x => x.Email == email));
        }

        public Task<TransientUser> GetByEmailVerificationKeyAsync(string emailVerificationKey)
        {
            return Task.FromResult(InMemoryTransientUsers.TransientUsers.SingleOrDefault(x => x.VerificationKey == emailVerificationKey));
        }

        public Task Update(TransientUser entity)
        {
            Delete(entity.Id);
            Add(entity);
            return Task.CompletedTask;
        }

        public Task<TransientUser> GetByUsernameAsync(string username)
        {
            return Task.FromResult(InMemoryTransientUsers.TransientUsers.SingleOrDefault(x => x.Username == username));
        }
    }
}