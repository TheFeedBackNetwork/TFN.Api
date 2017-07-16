using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using CacheManager.Core;
using CacheManager.Core.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TFN.Domain.Architecture.Attributes;

namespace TFN.Infrastructure.Architecture.Caching.Base
{
    public abstract class BaseCache<T>
        where T : class
    {
        protected abstract ICacheManager<T> CacheManager { get; }

        protected string Region { get; }

        protected int Version { get; }

        protected TimeSpan DefaultExpiration { get; }

        protected JsonSerializerSettings DefaultSerializerSettings { get; }

        protected ILogger Logger { get; }

        public BaseCache(string region, TimeSpan defaultExpiry, ILogger logger, bool isCollectionOf = false)
        {
            if (String.IsNullOrWhiteSpace(region))
            {
                throw new ArgumentNullException(nameof(region), "Cache region cannot be null or empty.");
            }

            if (defaultExpiry == TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(defaultExpiry), "Cache expiry cannot be zero.");
            }

            Region = region;
            DefaultExpiration = defaultExpiry;
            Logger = logger;

            CacheVersionAttribute cacheVersionAttribute;

            if (isCollectionOf)
            {
                if (typeof(T) is IEnumerable<object>)
                {
                    throw new ArgumentException(nameof(isCollectionOf),
                        $"Generic type '{typeof(T).Name}' if not a collection.");
                }

                var generic = typeof(T).GetGenericArguments().SingleOrDefault();

                if (generic == null)
                {
                    throw new InvalidOperationException("Expecting a generic type for the collection cache type.");
                }

                cacheVersionAttribute = generic
                    .GetCustomAttributes(false)
                    .OfType<CacheVersionAttribute>()
                    .SingleOrDefault();
            }
            else
            {
                cacheVersionAttribute = typeof(T)
                    .GetCustomAttributes(false)
                    .OfType<CacheVersionAttribute>()
                    .SingleOrDefault();
            }

            if (cacheVersionAttribute == null)
            {
                throw new InvalidOperationException(
                    $"Type '{typeof(T).Name}' must be decorated with [{typeof(CacheVersionAttribute).Name}] to be used in the distributed cache.");
            }

            Version = cacheVersionAttribute.Version;

            DefaultSerializerSettings = new JsonSerializerSettings()
            {
                // Use ISO 8601 for consistency.
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                // Store in UTC for consistency.
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                // Be explicit and serialise even if fields are set to their default value (i.e. null).
                DefaultValueHandling = DefaultValueHandling.Include,
                // Be explicit and serialise null values.
                NullValueHandling = NullValueHandling.Include,
                // Ignore fields in the serialised data which are not represented in the type (this could be a removed property from the entity).
                MissingMemberHandling = MissingMemberHandling.Ignore,
                // Do not store type details.
                TypeNameHandling = TypeNameHandling.None,
                // Be assembly agnostic.
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple
            };
        }

        protected T Get(string key)
        {
            key = GenerateVersionedKey(key, Version);

            return CacheManager.Get<T>(key, Region);
        }

        protected void Add(string key, T value)
        {
            key = GenerateVersionedKey(key, Version);

            var cacheItem = new CacheItem<T>(
                key,
                Region,
                value,
                ExpirationMode.Sliding,
                DefaultExpiration);

            CacheManager.AddOrUpdate(cacheItem, v => v);
        }

        protected void Remove(string key)
        {
            key = GenerateVersionedKey(key, Version);


            CacheManager.Remove(key,Region);
            
        }

        protected string GenerateVersionedKey(string key, int version)
        {
            if (version == 0)
            {
                return key;
            }
            else
            {
                return $"{key}:{version}";
            }
        }

        protected void PropagateOnRemoteAdd(object sender, CacheActionEventArgs e)
        {
            if (e.Origin == CacheActionEventArgOrigin.Remote && e.Region == Region)
            {
                // This will pull the item from Redis and cache it in-proc.
                var item = CacheManager.Get(e.Key, e.Region);

                if (item as T == null)
                {
                    throw new InvalidOperationException($"Unable to cast item of type '{item.GetType().Name}' to '{typeof(T).Name}'.");
                }
            }
        }

        protected void OnPut(object sender, CacheActionEventArgs e)
        {
            Logger.LogInformation($"[{System.Reflection.Assembly.GetEntryAssembly().ManifestModule.Name}] PUT {e.Region}.{e.Key} from {e.Origin}");
        }

        protected void OnUpdate(object sender, CacheActionEventArgs e)
        {
            Logger.LogInformation($"[{System.Reflection.Assembly.GetEntryAssembly().ManifestModule.Name}] UPDATE {e.Region}.{e.Key} from {e.Origin}");
        }

        protected void OnAdd(object sender, CacheActionEventArgs e)
        {
            Logger.LogInformation($"[{System.Reflection.Assembly.GetEntryAssembly().ManifestModule.Name}] ADD {e.Region}.{e.Key} from {e.Origin}");
        }

        protected void OnGet(object sender, CacheActionEventArgs e)
        {
            Logger.LogInformation($"[{System.Reflection.Assembly.GetEntryAssembly().ManifestModule.Name}] GET {e.Region}.{e.Key} from {e.Origin}");
        }
    }
}