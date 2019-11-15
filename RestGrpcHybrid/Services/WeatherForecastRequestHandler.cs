using RestGrpcHybrid.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestGrpcHybrid.Services
{
    public class WeatherForecastRequestHandler : IWeatherForecastRequestHandler
    {
        public IEnumerable<WeatherForecast> GetForecast()
        {
            var rng = new Random();
            return Enumerable.Range(1, 10).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55)
            }).ToArray();

        }

        public WeatherForecast GetSingleForecast(DateTime datetime)
        {
            return  new WeatherForecast()
            {
                Date = datetime,
                TemperatureC = 25
            };
        }
    }
}
