using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;
using TFN.Infrastructure.Architecture.Mapping;
using TFN.Infrastructure.Architecture.Repositories.Document;

namespace TFN.Infrastructure.Repositories.UserAccountAggregate.Document
{
    public class UserAccountDocumentRepository : DocumentRepository<UserAccount, UserAccountDocumentModel, Guid>, IUserAccountRepository
    {
        public UserAccountDocumentRepository(
            IAggregateMapper<UserAccount, UserAccountDocumentModel, Guid> mapper,
            DocumentContext context,
            ILogger<UserAccountDocumentRepository> logger)
            : base(mapper,context,logger)
        {
            
        }

        public async Task<bool> ChangePasswordKeyExists(string changePasswordKey)
        {
            var any = await Collection.Any(x => x.ChangePasswordKey == changePasswordKey);

            return any;
        }

        public async Task<UserAccount> FindByChangePasswordKey(string changePasswordKey)
        {
            var document = await Collection.Find(x => x.ChangePasswordKey == changePasswordKey);

            if (document == null)
            {
                return null;
            }

            var aggregate = Mapper.CreateFrom(document);

            return aggregate;
        }

        public async Task<UserAccount> FindByEmail(string email)
        {
            var document = await Collection.Find(x => x.NormalizedEmail == email.ToUpperInvariant());

            if (document == null)
            {
                return null;
            }

            var aggregate = Mapper.CreateFrom(document);

            return aggregate;
        }

        public async Task<UserAccount> FindByUsername(string username)
        {
            var document = await Collection.Find(x => x.Username == username.ToUpperInvariant());

            if (document == null)
            {
                return null;
            }

            var aggregate = Mapper.CreateFrom(document);

            return aggregate;
        }

    }
}