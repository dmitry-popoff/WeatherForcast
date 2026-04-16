using System.Text.Json;

namespace WeatherForcast.Models;

public sealed record ResponseObject(JsonElement? Data = default, ErrorDetails? Error = default)
{    
    public static ResponseObject Failure(ErrorDetails error) => new ResponseObject(default, error);
}

public sealed record Response<TResult>(TResult? Data = default, ErrorDetails? Error = default)
{
    public static Response<TResult> Success(TResult data) => new Response<TResult>(data, default);
    public static Response<TResult> Failure(ErrorDetails error) => new Response<TResult>(default, error);

    public static implicit operator Response<TResult>(TResult data) => Success(data);
    public static implicit operator Response<TResult>(ErrorDetails error) => Failure(error);
}
