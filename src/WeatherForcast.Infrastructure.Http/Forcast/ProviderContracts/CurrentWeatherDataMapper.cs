using System.Text.Json;
using WeatherForcast.Models;
using WeatherForcast.Models.Forcast;


namespace WeatherForcast.Infrastructure.Http.Forcast.ProviderContracts;

internal static class CurrentWeatherDataMapper
{
    public static Result<CurrentWeather> MapFrom(string jsonString)
    {
        if (string.IsNullOrWhiteSpace(jsonString)) return ErrorDetails.NotFound;

        using JsonDocument document = JsonDocument.Parse(jsonString);
        JsonElement root = document.RootElement;

        return MapFrom(root);
    }

    public static Result<CurrentWeather> MapFrom(JsonElement jsonElement) => new CurrentWeather
    {
        Location = GetLocation(jsonElement.GetProperty("location")),
        Weather = GetCurrentForcast(jsonElement.GetProperty("current")),
    };


    private static WeatherModel GetCurrentForcast(JsonElement forcast)
    {
        JsonElement cond = forcast.GetProperty(CurrentForcastTokens.Condition);

        var condition = new ConditionModel
        {
            Text = cond.GetProperty(ConditionTokens.Text).GetString() ?? string.Empty,
            Icon = cond.GetProperty(ConditionTokens.Icon).GetString() ?? string.Empty,
        };

        bool isParsed = DateTime.TryParse(
            forcast.GetProperty(CurrentForcastTokens.LastUpdated).GetString(),
            out DateTime time);

        return new WeatherModel
        {
            Condition = condition,
            IsDay = forcast.GetProperty(CurrentForcastTokens.IsDay).GetInt32() == 1,
            WindDegree = forcast.GetProperty(CurrentForcastTokens.WindDegree).GetInt32(),
            WindDirection = forcast.GetProperty(CurrentForcastTokens.WindDir).GetString() ?? string.Empty,
            Cloudness = forcast.GetProperty(CurrentForcastTokens.Cloud).GetInt32(),
            FeelslikeCelsius = forcast.GetProperty(CurrentForcastTokens.FeelslikeC).GetDecimal(),
            Humidity = forcast.GetProperty(CurrentForcastTokens.Humidity).GetInt32(),
            PressureMb = forcast.GetProperty(CurrentForcastTokens.PressureMb).GetDecimal(),
            TemperatureCelsius = forcast.GetProperty(CurrentForcastTokens.TempC).GetDecimal(),
            Time = isParsed ? time : DateTime.UtcNow,
            VisibilityKm = forcast.GetProperty(CurrentForcastTokens.VisKm).GetDecimal(),
            WindSpeedKmph = forcast.GetProperty(CurrentForcastTokens.WindKph).GetDecimal()
        };
    }

    private static LocationModel GetLocation(JsonElement location) => new LocationModel
    {
        Name = location.GetProperty(LocationTokens.Name).GetString() ?? string.Empty,
        Region = location.GetProperty(LocationTokens.Region).GetString() ?? string.Empty,
        Country = location.GetProperty(LocationTokens.Country).GetString() ?? string.Empty,
        Latitude = location.GetProperty(LocationTokens.Lat).GetDecimal(),
        Longitude = location.GetProperty(LocationTokens.Lon).GetDecimal(),
        Localtime = DateTime.Parse(location.GetProperty(LocationTokens.Localtime).GetString() ?? string.Empty)
    };
}
