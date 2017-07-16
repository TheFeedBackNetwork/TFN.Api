using System;
using CacheManager.Core;
using CacheManager.Core.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TFN.Domain.Architecture.Models;
using TFN.Infrastructure.Interfaces.Modules;
using TFN.Infrastructure.Architecture.Caching.Base;

namespace TFN.Infrastructure.Architecture.Caching.Aggregate
{
    public class AggregateCache<TAggregate>
        : Base.BaseCache<TAggregate>, IAggregateCache<TAggregate>
            where TAggregate : DomainEntity<Guid>, IAggregateRoot
    {
        // The aggregates will remain in cache for 4 days after their last cache hit (sliding expiration).
        public const int SlidingExpirationInDays = 4;
        private ICacheManager<TAggregate> cacheManager;
        private readonly RedisSettings settings;

        public AggregateCache(IOptions<RedisSettings> options, ILogger<AggregateCache<TAggregate>> logger)
            : base(typeof(TAggregate).Name,TimeSpan.FromDays(SlidingExpirationInDays),logger)
        {
            settings = options.Value;
        }

        protected override ICacheManager<TAggregate> CacheManager
        {
            get
            {
                if (cacheManager == null)
                {
                    if (settings == null)
                    {
                        throw new InvalidOperationException("Redis settings cannot be null or empty.");
                    }

                    cacheManager = CacheFactory.Build<TAggregate>(p => p
                        // Use compressed JSON serialisation. This will only apply to the Redis handler (in-proc doesn't serialise).
                        .WithGzJsonSerializer(DefaultSerializerSettings, DefaultSerializerSettings)
                        // Only retry 5 times.
                        .WithMaxRetries(5)
                        // The in-process cache will serve aggregates straight from memory.
                        .WithSystemRuntimeCacheHandle("in-process")
                        // Add the backplane Redis cache handle to establish a multi-layer cache.
                        .And
                        // Distributed cache (Azure Redis).
                        .WithRedisConfiguration("redis", $"{settings.Endpoint}:{settings.Port},password={settings.Password},ssl={settings.Ssl},abortConnect=False")
                        // Set as backplane to synchronise from the TransitApi.Chronos client.
                        .WithRedisBackplane("redis")
                        // CacheUpdateMode.Up will synchronise the in-process cache from additions to Redis.
                        .WithUpdateMode(CacheUpdateMode.Up)
                        // Use this backplane cache handle.
                        .WithRedisCacheHandle("redis", true)
                    );

                    // This will ensure the item is propogated down to the in-proc cache once added to Redis.
                    CacheManager.OnAdd += PropagateOnRemoteAdd;
                }

                return cacheManager;
            }
        }

        public void Add(TAggregate aggregate)
        {
            Add(aggregate.Id.ToString(),aggregate);
        }

        public TAggregate Find(Guid id)
        {
            return Find(id.ToString());
        }
    }
}