using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestGrpcHybrid.Interfaces;

namespace RestGrpcHybrid.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastRequestHandler _weatherForecastRequestHandler;

        public WeatherForecastController(IWeatherForecastRequestHandler weatherForecastRequestHandler)
        {
            _weatherForecastRequestHandler = weatherForecastRequestHandler;
        }

        [HttpGet("{datetime}")]
        public Task<WeatherForecast> GetSingleDayForecast(DateTime datetime)
        {
            return Task.FromResult(_weatherForecastRequestHandler.GetSingleForecast(datetime));
        }

        [HttpGet("longterm")]
        public Task<IEnumerable<WeatherForecast>> GetTenDaysForecast()
        {
            return Task.FromResult(_weatherForecastRequestHandler.GetForecast());
        }
    }
}
