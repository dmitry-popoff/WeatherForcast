using Microsoft.AspNetCore.Diagnostics;
using WeatherForcast.Models;

namespace WeatherForcast.WebApi.Middleware;

internal sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(
            exception, "Exception occurred: {Message}", exception.Message);

        var problemDetails = new ErrorDetails
        (            
            "Server error",
            StatusCodes.Status500InternalServerError.ToString()
        );

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails.ToResponse(), cancellationToken);

        return true;
    }
}
