﻿using MediatR;
using Serilog.Context;
using timeZZle.Shared.Utils;

namespace timeZZle.Behaviors;

internal sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>(
    ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class
        where TResponse : Result
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        logger.LogInformation("Processing request {RequestName}", requestName);

        var result = await next(cancellationToken);

        if (!result.IsSuccess)
        {
            using (LogContext.PushProperty("Error", result.Error, true))
            {
                logger.LogError("Completed request {RequestName} with error", requestName);
            }

            return result;
        }
        
        logger.LogInformation("Completed request {RequestName}", requestName);
        
        return result;
    }
}