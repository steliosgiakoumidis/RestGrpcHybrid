using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using RestGrpcHybrid.HelperMethods;
using RestGrpcHybrid.Interfaces;
using System.Threading.Tasks;
using WeatherForecastGrpc;
using static WeatherForecastGrpc.WeatherForecastService;

namespace GrpcServices
{
    public class WeatherForecastService : WeatherForecastServiceBase
    {
        private readonly IWeatherForecastRequestHandler _requestHandler;
        private readonly Transformers _transformers;

        public WeatherForecastService(IWeatherForecastRequestHandler requestHandler,
            Transformers transformers) 
        {
            _transformers = transformers;
            _requestHandler = requestHandler;
        }

        public override async Task GetWeatherGrpc(Empty _, IServerStreamWriter<WeatherData> responseStream, ServerCallContext context)
        {
            var weatherResult = _requestHandler.GetForecast();
            foreach (var forecast in weatherResult)
            {
                if (context.CancellationToken.IsCancellationRequested) return;
                await Task.Delay(200); // Gotta look busy
                await responseStream.WriteAsync(_transformers.WeatherForecastToGrpcModel(forecast));
            }
        }

        public override Task<WeatherData> GetWeatherSingleGrpc(GetWeatherRequest weatherRequest, ServerCallContext context)
        {
            var dateTime = weatherRequest.DateTime.ToDateTime();
            var weatherForecastForSingleDay = _requestHandler.GetSingleForecast(dateTime);
            return Task.FromResult(_transformers.WeatherForecastToGrpcModel(weatherForecastForSingleDay));
        }
    }
}
