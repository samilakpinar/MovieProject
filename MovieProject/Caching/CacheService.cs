using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace MovieProject.Caching
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;

        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<string> GetDataFromCache(string cacheKey)
        {
            var getFromCache = await _distributedCache.GetAsync(cacheKey);

            if (getFromCache == null)
            {
                return null;
            }

            return Encoding.UTF8.GetString(getFromCache);
        }

        public async Task SetDataToCache(string cacheKey, object response)
        {
            var cacheJsonItem = JsonConvert.SerializeObject(response);

            var byteFromCache = Encoding.UTF8.GetBytes(cacheJsonItem);

            var options = new DistributedCacheEntryOptions()
                //Absolute, datanın cache’de tutulma süresini belirler. 
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                //cachte tutulma süresini girilen değer kadar uzatır.
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));


            await _distributedCache.SetAsync(cacheKey, byteFromCache, options);

            return;
        }
    }
}
