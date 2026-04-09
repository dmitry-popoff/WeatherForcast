using WeatherForcast.Models.Forcast;

namespace WeatherForcast.Infrastructure.Http.Forcast.ProviderContracts;

internal static class ProviderContractExtensions
{
    public static DailyWeatherForcast Map(this Forecastday daily)
    {
        return new DailyWeatherForcast 
        {
            Date = DateOnly.Parse( daily.Date),
            Astro = daily.Astro.Map(),
            AverageForcast = daily.Day.Map(),
            Hourly = daily.Hour.Select(f => f.Map()).ToDictionary(f => f.Time.Hour)
        };
    }

    public static WeatherForcastModel Map(this Hour hourlyForcast) => new WeatherForcastModel
    {
        SnowChance = hourlyForcast.ChanceOfSnow,
        SnowCm = hourlyForcast.SnowCm,
        IsDay = hourlyForcast.IsDay == 1,
        WindSpeedKmph = hourlyForcast.WindKph,
        Cloudness = hourlyForcast.Cloud,
        WindDegree = hourlyForcast.WindDegree,
        WindDirection = hourlyForcast.WindDir,
        Condition = new ConditionModel 
        { 
            Icon = hourlyForcast.Condition.Icon,
            Text = hourlyForcast.Condition.Text,
        },
        FeelslikeCelsius = hourlyForcast.FeelslikeC,
        Humidity = hourlyForcast.Humidity,
        PressureMb = hourlyForcast.PressureMb,
        RainChance = hourlyForcast.ChanceOfRain,
        TemperatureCelsius = hourlyForcast.TempC,
        Time = DateTime.Parse(hourlyForcast.Time),
        VisibilityKm = hourlyForcast.VisKm
    };

    public static AggregatedWeatherForcastModel Map(this Day avg) => new AggregatedWeatherForcastModel
    {
        HumidityAvg = avg.Avghumidity,
        DailyChanceOfRain = avg.DailyChanceOfRain,
        DailyChanceOfSnow = avg.DailyChanceOfSnow,
        TemperatureCelsiusAvg = avg.AvgtempC,
        TemperatureCelsiusMax = avg.MaxtempC,
        TemperatureCelsiusMin = avg.MintempC,
        TotalPrecipMm = avg.TotalprecipMm,
        TotalSnowCm = avg.TotalsnowCm,
        WindSpeedKmphMax = avg.MaxwindKph,
        VisibilityKmAvg = avg.AvgvisKm
    };

    public static AstroModel Map(this Astro astro) => new AstroModel
    {
        MoonIllumination = astro.MoonIllumination,
        MoonPhaseTime = astro.MoonPhase,
        MoonriseTime = astro.Moonrise,
        MoonsetTime = astro.Moonset,
        SunriseTime = astro.Sunrise,
        SunsetTime = astro.Sunset
    };

    public static ForcastModel Map(this DailyForcastData forcast) => new ForcastModel
    {
        Location = forcast.Location.Map(),
        CurrentWeather = forcast.Current.Map(),
        Daily = forcast.Forecast.Forecastday
            .Select(f => f.Map())
            .ToDictionary(f => f.Date)
    };

    public static LocationModel Map(this Location location) => new LocationModel
    {
        Latitude = location.Lat,
        Longitude = location.Lon,
        Localtime = DateTime.Parse(location.Localtime),
        Country = location.Country,
        Name = location.Name,
        Region = location.Region
    };

    public static WeatherModel Map(this Current currentWeather) => new WeatherModel
    {
        Condition = new ConditionModel
        {
            Icon = currentWeather.Condition.Icon,
            Text = currentWeather.Condition.Text
        },
        IsDay = currentWeather.IsDay == 1,
        VisibilityKm = currentWeather.VisKm,
        WindDegree = currentWeather.WindDegree,
        WindDirection = currentWeather.WindDir,
        Cloudness = currentWeather.Cloud,
        FeelslikeCelsius = currentWeather.FeelslikeC,
        Humidity = currentWeather.Humidity,
        PressureMb = currentWeather.PressureMb,
        TemperatureCelsius = currentWeather.TempC,
        Time = DateTime.Parse( currentWeather.LastUpdated),
        WindSpeedKmph = currentWeather.WindKph
    };
}