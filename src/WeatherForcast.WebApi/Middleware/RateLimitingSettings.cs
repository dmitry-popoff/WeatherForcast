namespace WeatherForcast.WebApi.Middleware;

public sealed class RateLimitingSettings
{
    public const string Section = "RateLimitingSettings";
    public static RateLimitingSettings Default
    => new RateLimitingSettings
    {
        FixedWindowLimiter = FixedWindowLimiterSettings.Default
    };
    public required FixedWindowLimiterSettings FixedWindowLimiter { get; set; } = FixedWindowLimiterSettings.Default;
}

public sealed class FixedWindowLimiterSettings
{
    public const string Section = "FixedWindowLimiterSettings";

    public static FixedWindowLimiterSettings Default
        => new FixedWindowLimiterSettings
        {
            PermitLimit = 10,
            WindowLimitSec = 5,
            QueueLimit = 10
        };

    public int PermitLimit { get; set; }
    public int WindowLimitSec { get; set; }
    public int QueueLimit {  get; set; }
}
