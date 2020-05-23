using Enyim.Caching;
using System.Threading.Tasks;

namespace WebApplication.Memcached
{
    public interface ICacheRepository
    {
        ValueTask Set<T>(string key, T value);
    }

    public class CacheRepository : ICacheRepository
    {
        private readonly IMemcachedClient _memcachedClient;

        public CacheRepository(IMemcachedClient memcachedClient)
        {
            _memcachedClient = memcachedClient;
        }

        public async ValueTask Set<T>(string key, T value)
        {
            await _memcachedClient.SetAsync(key, value, 60 * 60);
        }
    }
}