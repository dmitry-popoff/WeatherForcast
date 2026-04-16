namespace WeatherForcast.Models.Forcast;

public static class WeatherForcastExtensions
{
    public static Response<CurrentWeather> Success(this CurrentWeather current) => current;

    public static Response<ForcastModel> Success(this ForcastModel forcast) => forcast;
}

