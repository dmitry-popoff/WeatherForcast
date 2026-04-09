using Microsoft.Extensions.Options;
using System.IO.Compression;
using System.Net.Http.Json;
using System.Text.Json;
using WeatherForcast.Infrastructure.Http.Forcast.ProviderContracts;
using WeatherForcast.Models;
using WeatherForcast.Models.Forcast;


namespace WeatherForcast.Infrastructure.Http.Forcast;
// https://api.weatherapi.com/v1/forecast.json?key=fa8b3df74d4042b9aa7135114252304&q=55.7512,37.6184&days=3
// https://api.weatherapi.com/v1/current.json?key=fa8b3df74d4042b9aa7135114252304&q=LAT,LON
internal sealed partial class ForcastClient : IForcastClient
{
    private readonly HttpClient _httpClient;
    private readonly ForcastSettings _settings;
    public ForcastClient(HttpClient httpClient, IOptions<ForcastSettings> options)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _settings = options.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public async ValueTask<Result<CurrentWeather>> GetAsync(CurrentForcastRequest request, CancellationToken cancellationToken = default)
    {
        var requestString = new ForcastRequestBuilder()
            .WithAddress(_settings.CurrentForcastUrl)
            .WithKey(_settings.Key)
            .WithLongitude(_settings.Longitude)
            .WithLatitude(_settings.Latitude)
            .Build();

        using HttpResponseMessage response = await _httpClient
            .GetAsync(requestString, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

        if (response.IsSuccessStatusCode is false)
        {
            return new ErrorDetails(
                string.IsNullOrWhiteSpace( response.ReasonPhrase) 
                    ? "Forcast provider return error" : response.ReasonPhrase,
                response.StatusCode.ToString());
        }

        using Stream stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        return CurrentWeatherDataMapper
            .MapFrom(await ReadFrom<JsonElement>(stream, "br", cancellationToken));
    }

    public async ValueTask<Result<ForcastModel>> GetAsync(ForcastRequest request, CancellationToken cancellationToken = default)
    {
        var requestString = new ForcastRequestBuilder()
            .WithAddress(_settings.DaysForcastUrl)
            .WithKey(_settings.Key)
            .WithLongitude(_settings.Longitude)
            .WithLatitude(_settings.Latitude)
            .Build();

        string uri = $"{requestString}&days={request.Days}";

        using HttpResponseMessage response = 
            await _httpClient            
                .GetAsync(uri, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

        if (response.IsSuccessStatusCode is false)
        {
            return new ErrorDetails(
                string.IsNullOrWhiteSpace(response.ReasonPhrase)
                    ? "Forcast provider return error" : response.ReasonPhrase,
                response.StatusCode.ToString());
        }

        DailyForcastData? forcast = default;

        if (response.Content is object)
        {
            using Stream stream = await response.Content.ReadAsStreamAsync(cancellationToken);

            forcast = await ReadFrom(stream, "br", cancellationToken);
        }

        return forcast is not null ? forcast.Map() : ErrorDetails.NotFound;
    }

    private async ValueTask<DailyForcastData?> ReadFrom(Stream stream, string encoding, CancellationToken cancellationToken)
    {
        if (encoding.Contains("br"))
        {
            using var brotliStream = new BrotliStream(stream, CompressionMode.Decompress);
            return await JsonSerializer.DeserializeAsync<DailyForcastData>(brotliStream);
        }
        return default;
    }

    private async ValueTask<T?> ReadFrom<T>(Stream stream, string encoding, CancellationToken cancellationToken)
    {
        if (encoding.Contains("br"))
        {
            using var brotliStream = new BrotliStream(stream, CompressionMode.Decompress);
            return await JsonSerializer.DeserializeAsync<T>(brotliStream);
        }
        return default;
    }
}
