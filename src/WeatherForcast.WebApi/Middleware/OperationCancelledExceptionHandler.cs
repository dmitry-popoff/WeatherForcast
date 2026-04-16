using Microsoft.AspNetCore.Diagnostics;
using WeatherForcast.Models;

namespace WeatherForcast.WebApi.Middleware;

internal sealed class OperationCancelledExceptionHandler : IExceptionHandler
{
    private readonly ILogger<OperationCancelledExceptionHandler> _logger;

    public OperationCancelledExceptionHandler(ILogger<OperationCancelledExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not OperationCanceledException) return false;

        _logger.LogError(
            exception, "Exception occurred: {Message}", exception.Message);

        var problemDetails = new ErrorDetails
        (
            "Operation was cancelled",
            StatusCodes.Status408RequestTimeout.ToString()
        );

        httpContext.Response.StatusCode = StatusCodes.Status408RequestTimeout;

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails.ToResponse(), cancellationToken);

        return true;
    }
}
