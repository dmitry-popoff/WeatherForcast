namespace WeatherForcast.Infrastructure.Http.Forcast;

public class ForcastSettings
{
    // https://api.weatherapi.com/v1/forecast.json?key=fa8b3df74d4042b9aa7135114252304&q=55.7512,37.6184&days=3
    // https://api.weatherapi.com/v1/current.json?key=fa8b3df74d4042b9aa7135114252304&q=LAT,LON

    public const string Section = "ForcastSettings";
    public required string Host { get; set; }
    public required string DaysForcastUrl {  get; set; }
    public required string CurrentForcastUrl { get; set; }
    public required string Key { get; set; }
    public required decimal Latitude { get; set; }
    public required decimal Longitude { get; set; }    
}

