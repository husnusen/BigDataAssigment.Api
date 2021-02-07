using System;
using System.Threading.Tasks;

namespace BigDataAssigment.Api.Caching
{
    public interface IMemoryCacheService
    {
        Task<T>  ReadFromCache<T>(string key);

        Task<T> SetCache<T>(string key, T entry);

    }
}
