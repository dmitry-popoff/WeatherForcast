namespace WeatherForcast.Models;

public static class ErrorDetailsExtensions
{
    public static Response<T> ToResponse<T>(this ErrorDetails error) => error;
    public static ResponseObject ToResponse(this ErrorDetails error) => ResponseObject.Failure(error);
}