using Enyim.Caching;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication.Memcached
{
    public interface ICacheProvider
    {
        ValueTask<T> Get<T>(string key);

        ValueTask<IEnumerable<T>> Get<T>(IEnumerable<string> keys);
    }

    public class CacheProvider : ICacheProvider
    {
        private readonly IMemcachedClient _memcachedClient;

        public CacheProvider(IMemcachedClient memcachedClient)
        {
            _memcachedClient = memcachedClient;
        }

        public async ValueTask<T> Get<T>(string key)
        {
            var valueResult = await _memcachedClient.GetAsync<T>(key);
            return valueResult.Value;
        }

        public ValueTask<IEnumerable<T>> Get<T>(IEnumerable<string> keys)
        {
            throw new NotImplementedException();
        }
    }
}