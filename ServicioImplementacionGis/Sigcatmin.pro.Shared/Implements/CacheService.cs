using Microsoft.Extensions.Caching.Memory;
using Sigcatmin.pro.Application.Interfaces;
using Sigcatmin.pro.Application.Contracts.Enums;

namespace Sigcatmin.pro.Shared.Implements
{
    public class CacheService: ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void SetValue<T>(CacheKeysEnum key, T value, TimeSpan? expiration = null)
        {
            var options = new MemoryCacheEntryOptions();
            if (expiration.HasValue)
            {
                options.SetAbsoluteExpiration(expiration.Value);
            }
            _memoryCache.Set(key, value, options);
        }

        public T GetValue<T>(CacheKeysEnum key)
        {
            return _memoryCache.Get<T>(key);
        }

        public bool ContainsKey(CacheKeysEnum key)
        {
            return _memoryCache.TryGetValue(key, out _);
        }

        public void RemoveValue(CacheKeysEnum key)
        {
            _memoryCache.Remove(key);
        }
    }
}
