using Microsoft.Extensions.Caching.Distributed;

namespace VtrEffects.Caching
{
    public interface ICachingService
    {



        Task SetAsync(string key, string values);
        Task<string> GetAsync(string key);
    }
}
