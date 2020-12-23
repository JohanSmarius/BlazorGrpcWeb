using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using BlazorGrpcWeb.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;



namespace BlazorGrpcWeb.Server.Services
{
    public class WeatherForecastServiceImpl : Protos.WeatherForecastService.WeatherForecastServiceBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        private readonly ILogger<WeatherForecastServiceImpl> _logger;

        public WeatherForecastServiceImpl(ILogger<WeatherForecastServiceImpl> logger)
        {
            _logger = logger;
        }

        public override Task<WeatherForecastResponse> GetForecasts(Empty request, ServerCallContext context)
        {
            var result = new WeatherForecastResponse();

            var rng = new Random();
            var forecasts = Enumerable.Range(1, 500).Select(index => new WeatherForecastDTO
            {
                Date = Timestamp.FromDateTime(DateTime.UtcNow.AddDays(index)),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray();
            
            result.Forecasts.AddRange(forecasts);

            return Task.FromResult(result);
        }
    }
}
