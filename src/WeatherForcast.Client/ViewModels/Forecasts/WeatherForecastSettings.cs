namespace WeatherForcast.Client.ViewModels.Forecasts;

public sealed class WeatherForecastSettings
{
    public const string Section = "WeatherForecastSettings";

    public static readonly WeatherForecastSettings Default = new();

    public string? BaseAddress { get; set; }
    public string? CurrentWeatherAddress { get; set; }
    public string? DailyForcastAddress { get; set; }
}
