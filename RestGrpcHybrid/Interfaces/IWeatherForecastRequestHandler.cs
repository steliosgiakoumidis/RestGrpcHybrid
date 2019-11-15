
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestGrpcHybrid.Interfaces
{
    public interface IWeatherForecastRequestHandler
    {
        WeatherForecast GetSingleForecast(DateTime datetime);
        IEnumerable<WeatherForecast> GetForecast();
    }
}
