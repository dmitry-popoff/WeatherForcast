using Microsoft.AspNetCore.Mvc;
using WeatherForcast.Infrastructure.Http.Forcast;
using WeatherForcast.Models;
using WeatherForcast.Models.Forcast;
using WeatherForcast.WebApi.Abstractions;

namespace WeatherForcast.WebApi.Features.Forecast.GetCurrent;


public record struct Request(
    [FromQuery(Name = "lat")] decimal Latitude = Request.DefaultLatitude,
    [FromQuery(Name = "lon")] decimal Longitude = Request.DefaultLongitude)
{
    public const decimal DefaultLatitude = 55.7512M;
    public const decimal DefaultLongitude = 37.6184M;
}

public class GetCurrentWeatherEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app) => app.MapGet("forcast/current",
            async (
                [AsParameters] Request request,
                [FromServices] IForcastClient forcastClient,
                CancellationToken cancellationToken) =>
            {
                var query = new CurrentForcastRequest(
                    new GeoLocation(latitude: request.Latitude, longitude: request.Longitude));

                Result<CurrentWeather> result = await forcastClient.GetAsync(query, cancellationToken);

                return result.IsSuccess
                    ? Results.Ok(result.Value.ToResponse())
                    : Results.BadRequest(result.Error.ToResponse());
            })
            .WithName("GetCurrentWeatherForecast")
            .RequireCors("AllowAll");
}
