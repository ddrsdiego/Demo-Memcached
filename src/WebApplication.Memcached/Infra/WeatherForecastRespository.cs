using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Memcached.Infra
{
    public interface IWeatherForecastRespository
    {
        Task<WeatherForecast> Get(string id);
    }

    public class WeatherForecastRespository : IWeatherForecastRespository
    {
        private static IEnumerable<WeatherForecast> WeatherForecasts
        {
            get
            {
                var rng = new Random();
                return new List<WeatherForecast>
                {
                    new WeatherForecast
                    {
                        WeatherForecastId = "1",
                        Date = DateTime.Now.AddDays(1),
                        TemperatureC = rng.Next(-20, 55),
                        Summary = Summaries[rng.Next(Summaries.Length)]
                    },
                    new WeatherForecast
                    {
                        WeatherForecastId = "2",
                        Date = DateTime.Now.AddDays(2),
                        TemperatureC = rng.Next(-20, 55),
                        Summary = Summaries[rng.Next(Summaries.Length)]
                    },
                    new WeatherForecast
                    {
                        WeatherForecastId = "3",
                        Date = DateTime.Now.AddDays(3),
                        TemperatureC = rng.Next(-20, 55),
                        Summary = Summaries[rng.Next(Summaries.Length)]
                    },
                    new WeatherForecast
                    {
                        WeatherForecastId = "4",
                        Date = DateTime.Now.AddDays(4),
                        TemperatureC = rng.Next(-20, 55),
                        Summary = Summaries[rng.Next(Summaries.Length)]
                    }
                };
            }
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public async Task<WeatherForecast> Get(string id)
        {
            await Task.CompletedTask;

            return WeatherForecasts.FirstOrDefault(x => x.WeatherForecastId.Equals(id));
        }
    }
}