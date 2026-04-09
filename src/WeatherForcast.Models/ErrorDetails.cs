namespace WeatherForcast.Models;

public partial record ErrorDetails(string Message, string ErrorCode)
{
    public static readonly ErrorDetails None = new(string.Empty, string.Empty);
    public static readonly ErrorDetails NullValue = new("The specified result value is null.", "Error.NullValue");
    public static readonly ErrorDetails BadRequest = new("Forecast provider return Bad Request", "Error.BadRequest");
    public static readonly ErrorDetails NotFound = new("Forecast provider return NotFound error", "Error.NotFound");
    public static readonly ErrorDetails ServerError = new("Forecast provider return ServerError", "Error.ServerError");
}
