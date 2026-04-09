using WeatherForcast.Models;
using WeatherForcast.Models.Forcast;


namespace WeatherForcast.Infrastructure.Http.Forcast;

public interface IForcastClient
{
    ValueTask<Result<CurrentWeather>> GetAsync(CurrentForcastRequest request, CancellationToken cancellationToken = default);
    ValueTask<Result<ForcastModel>> GetAsync(ForcastRequest request, CancellationToken cancellationToken = default);
}

public readonly struct GeoLocation
{
    public GeoLocation(decimal latitude, decimal longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public decimal Latitude {  get; }
    public decimal Longitude { get; }
}

public readonly struct CurrentForcastRequest
{
    public CurrentForcastRequest(GeoLocation location)
    {
        Location = location;
    }

    public GeoLocation Location { get; }
}

public readonly struct ForcastRequest
{
    public ForcastRequest(GeoLocation location, int days = 3)
    {
        Location = location;
        Days = days;
    }

    public GeoLocation Location { get; }
    public int Days { get; }

    public override string ToString()
    {
        return string.Format(
            "{0}-{1}-{2}-{3}-{4}-{5}",
            nameof(ForcastRequest),
            nameof(GeoLocation),
            Location.Longitude, 
            Location.Latitude,
            nameof(Days),
            Days
            );
    }
}
