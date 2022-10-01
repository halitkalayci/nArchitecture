using Core.Application.Pipelines.Caching;
using Core.CrossCuttingConcerns.Logging.Serilog;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.Performance
{
    public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, IPerformanceRequest
    {
        private readonly ILogger<PerformanceBehavior<TRequest, TResponse>> _logger;

        public PerformanceBehavior(ILogger<PerformanceBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            TResponse response = await next();
            stopwatch.Stop();
            string message = $"{request} request is handled in {stopwatch.Elapsed.TotalSeconds} seconds.";
            if (stopwatch.Elapsed.TotalSeconds > 10)
                _logger.LogCritical($"PERFORMANCE ISSUE: {message}");
            else
                _logger.LogInformation(message);
            return response;
        }
    }
}
