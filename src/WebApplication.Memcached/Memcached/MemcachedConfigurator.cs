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
            const int PORT_DEFAULT = 11211;
            const string ADDRESS_DEFAULT = "localhost";

            var sp = services.BuildServiceProvider();
            IOptions<MemcachedSettings> memcachedSettings = sp.GetService<IOptions<MemcachedSettings>>();

            if (memcachedSettings.Value is null)
                return new Server { Address = ADDRESS_DEFAULT, Port = PORT_DEFAULT };

            return new Server { Address = memcachedSettings.Value.Address, Port = memcachedSettings.Value.Port };
        }
    }
}