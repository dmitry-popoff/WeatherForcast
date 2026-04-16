namespace WeatherForcast.Models.Forcast;

public static class WeatherForcastExtensions
{
    public static Response<CurrentWeather> ToResponse(this CurrentWeather current) => current;

    public static Response<ForcastModel> ToResponse(this ForcastModel forcast) => forcast;
}

