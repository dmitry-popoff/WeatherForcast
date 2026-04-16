using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;
using WeatherForcast.Models;
using WeatherForcast.Models.Forcast;

namespace WeatherForcast.Client.ViewModels.Forecasts;

public class ForecastHttpClient: IWeatherForecastProvider
{
    private readonly HttpClient _httpClient;
    private readonly WeatherForecastSettings _settings;

    public ForecastHttpClient(HttpClient httpClient, IOptions<WeatherForecastSettings> options)
    {
        ArgumentNullException.ThrowIfNull(options);
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _settings = options.Value;
    }

    public async ValueTask<Result<CurrentWeather>> GetCurrentWeather(CancellationToken cancellationToken)
    {
        using HttpResponseMessage response = await _httpClient
                .GetAsync(_settings.CurrentWeatherAddress, cancellationToken);

        var responseObject = await response.Content.ReadFromJsonAsync<ResponseObject>(cancellationToken);

        if (response.IsSuccessStatusCode is false) 
        {
            return responseObject?.Error ?? new ErrorDetails("Error occured", response.StatusCode.ToString()); 
        }

        return responseObject is not null && responseObject.Data.HasValue
            ? JsonSerializer.Deserialize<CurrentWeather>(responseObject.Data.Value)
            : ErrorDetails.NotFound;
    }

    public async ValueTask<Result<ForcastModel>> GetDailyForcast(CancellationToken cancellationToken)
    {
        using HttpResponseMessage response = await _httpClient
            .GetAsync(_settings.DailyForcastAddress, cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return ErrorDetails.NotFound;
        }

        var responseObject = await response.Content.ReadFromJsonAsync<ResponseObject>(cancellationToken);

        if (response.IsSuccessStatusCode is false)
        {
            return responseObject?.Error ?? new ErrorDetails("Error occured", response.StatusCode.ToString());
        }

        return responseObject is not null && responseObject.Data.HasValue
            ? JsonSerializer.Deserialize<ForcastModel>(
                responseObject.Data.Value,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase })
            : ErrorDetails.NotFound;
    }
}