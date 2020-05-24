using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Memcached.Infra;

namespace WebApplication.Memcached.Controllers
{
    [ApiController]
    [Route("memcached/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ICacheProvider _cacheProvider;
        private readonly ICacheRepository _cacheRepository;
        private readonly IWeatherForecastRespository _weatherForecastRespository;

        public WeatherForecastController(ICacheProvider cacheProvider,
                                         ICacheRepository cacheRepository,
                                         IWeatherForecastRespository weatherForecastRespository)
        {
            _cacheProvider = cacheProvider;
            _cacheRepository = cacheRepository;
            _weatherForecastRespository = weatherForecastRespository;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var rng = new Random();

            var newWeatherForecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                WeatherForecastId = rng.Next(1, 9999).ToString(),
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).First();

            await _cacheRepository.Set(newWeatherForecast.WeatherForecastId, newWeatherForecast);

            return Created("", newWeatherForecast);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string value)
        {
            var test = await _cacheProvider.GetValueOrCreate(value, async () => await _weatherForecastRespository.Get(value));

            var cacheItem = await _cacheProvider.Get<WeatherForecast>(value);
            if (cacheItem is null)
                return NoContent();

            return Ok(cacheItem);
        }
    }
}