﻿using System.Threading.Tasks;
using TFN.Domain.Architecture.Models;

namespace TFN.Domain.Architecture.Repositories
{
    public interface IAddableRepository<TDomainEntity, TKey> : IRepository<TDomainEntity, TKey>
       where TDomainEntity : DomainEntity<TKey>, IAggregateRoot
    {

        Task Add(TDomainEntity domainEntity);

    }
}