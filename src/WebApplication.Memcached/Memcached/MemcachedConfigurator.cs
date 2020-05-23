using Enyim.Caching.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace WebApplication.Memcached
{
    public static class MemcachedConfigurator
    {
        public static IServiceCollection AddMemcached(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEnyimMemcached(o => o.Servers = new List<Server>
            {
                CreateServerConfig(services)
            });

            services.AddSingleton<ICacheProvider, CacheProvider>();
            services.AddSingleton<ICacheRepository, CacheRepository>();

            return services;
        }

        private static Server CreateServerConfig(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            IOptions<MemcachedSettings> memcachedSettings = sp.GetService<IOptions<MemcachedSettings>>();

            if (memcachedSettings is null)
                return new Server { Address = "localhost", Port = 11211 };

            return new Server { Address = memcachedSettings.Value.Address, Port = memcachedSettings.Value.Port };
        }
    }
}