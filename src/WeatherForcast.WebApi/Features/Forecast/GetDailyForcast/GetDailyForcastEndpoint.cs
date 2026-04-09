using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Hybrid;
using Polly;
using WeatherForcast.Infrastructure.Http.Forcast;
using WeatherForcast.Models;
using WeatherForcast.Models.Forcast;
using WeatherForcast.WebApi.Abstractions;

namespace WeatherForcast.WebApi.Features.Forecast.GetDailyForcast;

public record struct Request(
    [FromQuery(Name = "lat")] decimal Latitude = Request.DefaultLatitude,
    [FromQuery(Name = "lon")] decimal Longitude = Request.DefaultLongitude,
    [FromQuery(Name = "days")] int Days = Request.DefaultDays)
{
    public const decimal DefaultLatitude = 55.7512M;
    public const decimal DefaultLongitude = 37.6184M;
    public const int DefaultDays = 3;
}

public record Response(ForcastModel? Forcast = default, ErrorDetails? Error = default)
{
    public static Response Success(ForcastModel forcast) => new Response(forcast, null);
    public static Response Failure(ErrorDetails error) => new Response(null, error);

    public static implicit operator Response(ForcastModel forcast) => Success(forcast);
    public static implicit operator Response(ErrorDetails error) => Failure(error);
}

public class GetDailyForcastEndpoint : IEndpoint
{
    private const int RequestTimeoutSec = 20;
    public void MapEndpoint(IEndpointRouteBuilder app) 
        => app.MapGet("forcast/daily",
            async (
                [AsParameters] Request request,
                [FromServices] IForcastClient forcastClient,
                [FromServices] ILogger< GetDailyForcastEndpoint > logger,
                [FromServices] HybridCache cache,
                CancellationToken cancellationToken) =>
            {
                var query = new ForcastRequest(
                    new GeoLocation(latitude: request.Latitude, longitude: request.Longitude), request.Days);

                using var timeoutSource = new CancellationTokenSource(TimeSpan.FromSeconds(RequestTimeoutSec));

                using var linkedSource = CancellationTokenSource
                    .CreateLinkedTokenSource(cancellationToken, timeoutSource.Token);

                var resiliencyPipe = new ResiliencePipelineBuilder()
                    .AddTimeout(TimeSpan.FromSeconds(RequestTimeoutSec))
                    .Build();

                ForcastModel? forcast =
                await resiliencyPipe.ExecuteAsync(
                    async cancel => await cache.GetOrCreateAsync(
                        query.ToString(),
                        async ct =>
                        {
                            Result<ForcastModel> result = await forcastClient.GetAsync(query, linkedSource.Token);

                            return result.IsSuccess 
                                ? result.Value
                                : ErrorDetails.NotFound.Equals(result.Error)
                                    ? null
                                    : throw new InvalidOperationException(result.Error.Message);
                        },
                        null, null, cancel),
                    linkedSource.Token);               

                return forcast is not null ? Results.Ok(forcast) : Results.NotFound();
            })
            .WithName("GetDailyWeatherForecast");
}

