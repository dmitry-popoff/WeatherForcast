using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using WeatherForcast.WebApi.Middleware;

namespace Microsoft.Extensions.DependencyInjection;

internal static partial class ServiceCollectionExtensions
{
    internal static IServiceCollection AddMiddleware(this IServiceCollection services)
    {
        services.AddExceptionHandlers();

        return services;
    }

    internal static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<OperationCancelledExceptionHandler>();

        services.AddExceptionHandler<GlobalExceptionHandler>();

        return services;
    }

    internal static IServiceCollection AddRateLimiting(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RateLimitingSettings>(
            configuration.GetSection(RateLimitingSettings.Section));

        var limiterOptions = RateLimitingSettings.Default;
        configuration.GetSection(RateLimitingSettings.Section).Bind(limiterOptions);

        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            options.AddPolicy<string, FixedRateLimiterPolicy>("fixed");

        });

        return services;
    }

}
