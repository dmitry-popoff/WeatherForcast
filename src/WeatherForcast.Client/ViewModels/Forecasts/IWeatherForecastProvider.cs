using WeatherForcast.Models;
using WeatherForcast.Models.Forcast;

namespace WeatherForcast.Client.ViewModels.Forecasts;

public interface IWeatherForecastProvider
{
    ValueTask<Result<ForcastModel>> GetDailyForcast(CancellationToken cancellationToken);
    ValueTask<Result<CurrentWeather>> GetCurrentWeather(CancellationToken cancellationToken);
}
