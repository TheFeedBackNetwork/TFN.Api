﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;
using TFN.Infrastructure.Architecture.Mapping;
using TFN.Infrastructure.Architecture.Repositories.Cache;
using TFN.Infrastructure.Architecture.Repositories.Document;
using TFN.Infrastructure.Interfaces.Modules;

namespace TFN.Infrastructure.Repositories.UserAccountAggregate.Document
{
    public class UserAccountDocumentRepository : CachedDocumentRepository<UserAccount, UserAccountDocumentModel, Guid>, IUserAccountRepository
    {
        public UserAccountDocumentRepository(
            IAggregateMapper<UserAccount, UserAccountDocumentModel, Guid> mapper,
            DocumentContext context,
            ILogger<UserAccountDocumentRepository> logger,
            IAggregateCache<UserAccount> cache)
            : base(mapper, cache,context,logger)
        {
            
        }

        public async Task<bool> ChangePasswordKeyExists(string changePasswordKey)
        {
            var any = await Collection.Any(x => x.ChangePasswordKey == changePasswordKey && x.Type == Type);

            return any;
        }

        public async Task<UserAccount> FindByChangePasswordKey(string changePasswordKey)
        {
            var document = await Collection.Find(x => x.ChangePasswordKey == changePasswordKey && x.Type == Type);

            if (document == null)
            {
                return null;
            }

            var aggregate = Mapper.CreateFrom(document);

            return aggregate;
        }

        public async Task<UserAccount> FindByEmail(string email)
        {
            var document = await Collection.Find(x => x.NormalizedEmail == email.ToUpperInvariant() && x.Type == Type);

            if (document == null)
            {
                return null;
            }

            var aggregate = Mapper.CreateFrom(document);

            return aggregate;
        }

        public async Task<UserAccount> FindByUsername(string username)
        {
            var document = await Collection.Find(x => x.NormalizedUsername == username.ToUpperInvariant() && x.Type == Type);

            if (document == null)
            {
                return null;
            }

            var aggregate = Mapper.CreateFrom(document);

            return aggregate;
        }

    }
}