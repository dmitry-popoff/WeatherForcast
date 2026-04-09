namespace WeatherForcast.Models.Forcast;

public sealed class ForcastModel
{
    public required LocationModel Location { get; init; }
    public required WeatherModel CurrentWeather { get; init; }
    public Dictionary<DateOnly, DailyWeatherForcast>? Daily {  get; init; }
}


public sealed class DailyWeatherForcast
{
    public DateOnly Date { get; init; }
    public required AggregatedWeatherForcastModel AverageForcast { get; init; }
    public required AstroModel Astro { get; init; }
    public Dictionary<int, WeatherForcastModel>? Hourly {  get; init; }
}


public sealed class CurrentWeather
{
    public required LocationModel Location { get; init; }
    public required WeatherModel Weather { get; init; }
}

public class ConditionModel
{
    public string? Text { get; init; }
    public string? Icon { get; init; }    
}

public class WeatherModel
{
    public DateTime Time { get; init; }
    public decimal TemperatureCelsius { get; init; }
    public bool IsDay { get; init; }
    public required ConditionModel Condition { get; init; }
    public decimal WindSpeedKmph { get; init; }
    public int WindDegree { get; init; }
    public required string WindDirection { get; init; }
    public decimal PressureMb { get; init; }
    public int Humidity { get; init; }
    public int Cloudness { get; init; }
    public decimal FeelslikeCelsius { get; init; }
    public decimal VisibilityKm { get; init; }
}

public class WeatherForcastModel
{
    public DateTime Time { get; init; }
    public decimal TemperatureCelsius { get; init; }
    public bool IsDay { get; init; }
    public required ConditionModel Condition { get; init; }
    public decimal WindSpeedKmph { get; init; }
    public int WindDegree { get; init; }
    public required string WindDirection { get; init; }
    public decimal PressureMb { get; init; }
    public decimal SnowCm { get; init; }
    public int Humidity { get; init; }
    public int Cloudness { get; init; }
    public decimal FeelslikeCelsius { get; init; }
    public int RainChance { get; init; }
    public int SnowChance { get; init; }
    public decimal VisibilityKm { get; init; }
}

public class LocationModel
{
    public required string Name { get; init; }
    public required string Region { get; init; }
    public required string Country { get; init; }
    public decimal Latitude { get; init; }
    public decimal Longitude { get; init; }
    public DateTime Localtime { get; init; }
}

/// <summary>
/// Weather Forcast summary for day
/// </summary>
public class AggregatedWeatherForcastModel
{
    public decimal TemperatureCelsiusMax { get; init; }
    public decimal TemperatureCelsiusMin { get; init; }
    public decimal TemperatureCelsiusAvg { get; init; }
    public decimal WindSpeedKmphMax { get; init; }
    public decimal TotalPrecipMm { get; init; }
    public decimal TotalSnowCm { get; init; }
    public decimal VisibilityKmAvg { get; init; }
    public int HumidityAvg { get; init; }
    public int DailyChanceOfRain { get; init; }
    public int DailyChanceOfSnow { get; init; }
}

public class AstroModel
{
    public required string SunriseTime { get; init; }
    public required string SunsetTime { get; init; }
    public required string MoonriseTime { get; init; }
    public required string MoonsetTime { get; init; }
    public required string MoonPhaseTime { get; init; }
    public int MoonIllumination { get; init; }
}

