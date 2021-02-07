using System;
using Microsoft.Extensions.Caching.Memory;

namespace BigDataAssigment.Api.Caching
{
    public class AppMemoryCache
    {
        public MemoryCache Cache { get; set; }
        public AppMemoryCache()
        {
            Cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = Constants.Constants.CacheSize
            }) ;
        }
    }
}
