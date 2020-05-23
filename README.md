# SQS Demo Memcached

Um simples exemplo de como utilizar o Memcached em aplica��es ASP.NET Core.


## O que � o Memcached
O Memcached � um banco de dados chave e valor de c�digo aberto e alto desempenho, de mem�ria distribu�da, destinado a acelerar aplica��es Web, aliviando a carga do banco de dados.
https://memcached.org/

## Docker
Para o exemplo foi usada a imagem do Memcached, para rodar o container, seguir o comando: `docker run -p 11211:11211 -d memcached`

## Packages
A aplica��o faz uso do package `EnyimMemcachedCore`
https://www.nuget.org/packages/EnyimMemcachedCore/


### Rodar a aplica��o

**WARNING**
Caso n�o seja definido a configura��o abaixo no appSettings, a aplica��o ir� ser configurada para rodar localmente.

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
            var sp = services.BuildServiceProvider();
            IOptions<MemcachedSettings> memcachedSettings = sp.GetService<IOptions<MemcachedSettings>>();

            if (memcachedSettings is null)
                return new Server { Address = "localhost", Port = 11211 };

            return new Server { Address = memcachedSettings.Value.Address, Port = memcachedSettings.Value.Port };
        }
    }
```