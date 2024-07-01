using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreData.Interfaces
{
    public interface IresponseCacheService
    {
        Task CacheResponseAsync(string cacheKey, object response,TimeSpan timeToLive);
        Task<string> GetCachedResponseAsync(string cacheKey);
    }
}