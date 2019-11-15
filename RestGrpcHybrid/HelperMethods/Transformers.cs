using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecastGrpc;

namespace RestGrpcHybrid.HelperMethods
{
    public class Transformers
    {
        public WeatherData WeatherForecastToGrpcModel(WeatherForecast forecast)
        {
            return new WeatherData()
            {
                DateTime = Timestamp.FromDateTime(forecast.Date.ToUniversalTime()),
                TemperatureC = forecast.TemperatureC
            };
        }
    }
}
