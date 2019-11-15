using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecastGrpc;

namespace GrpcTestClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var forecastClient = new WeatherForecastService.WeatherForecastServiceClient(channel);

            //Single call 
            Console.WriteLine("Welcome to the gRPC client");
            var reply = forecastClient.GetWeatherSingleGrpc(new GetWeatherRequest
            {
                DateTime = Timestamp.FromDateTime(DateTime.UtcNow.Date)
            });
            Console.WriteLine($"Created order: {reply.DateTime}");
            Console.WriteLine($"Created order: {reply.TemperatureC}");

            //Stream call
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            using var streamingCall = forecastClient.GetWeatherGrpc(new Empty(), cancellationToken: cts.Token);
            try
            {
                await foreach (var weatherData in streamingCall.ResponseStream.ReadAllAsync(cancellationToken: cts.Token))
                {
                    Console.WriteLine($"{weatherData.DateTime.ToDateTime():s} | {weatherData.TemperatureC} C");
                }
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
            {
                Console.WriteLine("Stream cancelled.");
            }

            Console.ReadKey();
        }
    }
}
