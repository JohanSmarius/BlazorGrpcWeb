using System;
using System.Drawing;
using System.Threading.Tasks;
using BlazorGrpcWeb.Protos;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace BlazorGrpcWeb.Server.Services
{
    public class EchoServiceImpl : EchoService.EchoServiceBase
    {
        private readonly ILogger<EchoServiceImpl> _logger;

        public EchoServiceImpl(ILogger<EchoServiceImpl> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override Task<EchoResponse> EchoMessage(EchoRequest request, ServerCallContext context)
        {
            var responseMessage = $"Echo: {request.Name}";

            var echoResponse = new EchoResponse { Name = responseMessage };

            return Task.FromResult<EchoResponse>(echoResponse);

        }
    }
}