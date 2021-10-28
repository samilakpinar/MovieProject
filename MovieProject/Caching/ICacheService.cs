using System.Threading.Tasks;

namespace MovieProject.Caching
{
    public interface ICacheService
    {
        Task SetDataToCache(string cacheKey, object response);
        Task<string> GetDataFromCache(string cacheKey);
    }
}
