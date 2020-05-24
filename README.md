# Demo Memcached

Um simples exemplo de como utilizar o Memcached em aplicações ASP.NET Core.


## O que é o Memcached
O Memcached é um banco de dados chave e valor de código aberto e alto desempenho, de memória distribuída, destinado a acelerar aplicações Web, aliviando a carga do banco de dados.

Para mais informações consultar o site: https://memcached.org/

## Docker
Para o exemplo foi usada a imagem do Memcached, para rodar o container, seguir o comando:

`docker run -p 11211:11211 -d memcached`

## Packages
A aplicação faz uso do NuGet package `EnyimMemcachedCore`
https://www.nuget.org/packages/EnyimMemcachedCore/


### Rodar a aplicação

**WARNING**
Caso não seja definido a configuração abaixo no appSettings, a aplicação irá ser configurada para rodar localmente.

```json
  "MemcachedSettings": {
    "Port": 11211,
    "Address": "localhost"
  }
```


```csharp
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
```
