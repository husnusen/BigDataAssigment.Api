using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace BigDataAssigment.Api.Caching
{
    public class MemoryCacheService :IMemoryCacheService
    {

        private AppMemoryCache _appMemoryCache;
        public MemoryCacheService(AppMemoryCache appMemoryCache)
        {
            _appMemoryCache = appMemoryCache;
        }

        public  async Task<T> ReadFromCache<T>(string key)
        {
            T entry;
           _appMemoryCache.Cache.TryGetValue<T>(key, out entry);
            return await Task.FromResult(entry);

        }

        public async Task<T> SetCache<T>(string key, T entry)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                       .SetSize(1)
                       .SetSlidingExpiration(TimeSpan.FromSeconds(60));
            if (_appMemoryCache.Cache.Count > Constants.Constants.CacheSize) {
                _appMemoryCache.Cache.Compact(10);
            }

            return await Task.FromResult(_appMemoryCache.Cache.Set(key, entry, cacheEntryOptions) );

        }
    }
}
