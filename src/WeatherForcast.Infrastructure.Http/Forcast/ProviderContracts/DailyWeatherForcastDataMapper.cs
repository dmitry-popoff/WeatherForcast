using System.Text.Json;
using WeatherForcast.Models;
using WeatherForcast.Models.Forcast;

namespace WeatherForcast.Infrastructure.Http.Forcast.ProviderContracts;

internal static class DailyWeatherForcastDataMapper
{
    public static Result<ForcastModel> MapFrom(string jsonString)
    {
        if (string.IsNullOrWhiteSpace(jsonString)) return ErrorDetails.NotFound;

        var forcast = JsonSerializer.Deserialize<DailyForcastData>(jsonString);

        if (forcast is null) return ErrorDetails.NotFound;

        return forcast.Map();
    }

    public static Result<ForcastModel> MapFrom(JsonElement json)
    {
        var forcast = JsonSerializer.Deserialize<DailyForcastData>(json);

        if (forcast is null) return ErrorDetails.NotFound;

        return forcast.Map();
    }

}
