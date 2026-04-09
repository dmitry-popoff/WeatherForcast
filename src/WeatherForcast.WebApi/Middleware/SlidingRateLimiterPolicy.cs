using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Threading.RateLimiting;

namespace WeatherForcast.WebApi.Middleware;

public class FixedRateLimiterPolicy : IRateLimiterPolicy<string>
{
    private Func<OnRejectedContext, CancellationToken, ValueTask>? _onRejected;
    private readonly RateLimitingSettings _settings;

    public FixedRateLimiterPolicy(ILogger<FixedRateLimiterPolicy> logger, IOptions<RateLimitingSettings> options)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(options);
        _onRejected = (ctx, token) =>
        {
            if (ctx.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
            {
                ctx.HttpContext.Response.Headers.RetryAfter =
                    ((int)retryAfter.TotalSeconds).ToString(NumberFormatInfo.InvariantInfo);
            }
            ctx.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            logger.LogWarning($"Request rejected by {nameof(FixedRateLimiterPolicy)}");
            return ValueTask.CompletedTask;
        };
        _settings = options.Value;
    }

    public Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected => _onRejected;

    public RateLimitPartition<string> GetPartition(HttpContext httpContext)
    {
        return RateLimitPartition.GetFixedWindowLimiter("fixed",
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = _settings.FixedWindowLimiter.PermitLimit,
                Window = TimeSpan.FromSeconds(_settings.FixedWindowLimiter.WindowLimitSec),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = _settings.FixedWindowLimiter.QueueLimit
            });
    }
}