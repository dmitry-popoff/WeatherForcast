using System.Text.Json.Serialization;

namespace WeatherForcast.Infrastructure.Http.Forcast.ProviderContracts;

public sealed class ConditionData
{
    [JsonPropertyName("text")]
    public string Text;

    [JsonPropertyName("icon")]
    public string Icon;

    [JsonPropertyName("code")]
    public int Code;
}

