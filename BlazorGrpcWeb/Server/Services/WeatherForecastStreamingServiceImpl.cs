using System;
using System.Linq;
using System.Threading.Tasks;
using BlazorGrpcWeb.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace BlazorGrpcWeb.Server.Services
{
    public class WeatherForecastStreamingServiceImpl : WeatherForecastStreamingService.WeatherForecastStreamingServiceBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastStreamingServiceImpl> _logger;

        public WeatherForecastStreamingServiceImpl(ILogger<WeatherForecastStreamingServiceImpl> logger)
        {
            _logger = logger;
        }

        public override async Task GetWeatherStream(Empty request, IServerStreamWriter<WeatherForecastStreamDTO> responseStream, ServerCallContext context)
        {
            var randomGenerator = new Random();
            var index = 0;

            while (!context.CancellationToken.IsCancellationRequested)
            {
                await Task.Delay(500);
                
                var forecast = new WeatherForecastStreamDTO()
                {
                    Date = Timestamp.FromDateTime(DateTime.UtcNow.AddDays(index++)),
                    TemperatureC = randomGenerator.Next(-20, 55),
                    Summary = Summaries[randomGenerator.Next(Summaries.Length)]
                };

                await responseStream.WriteAsync(forecast);
            }

        }
    }
}