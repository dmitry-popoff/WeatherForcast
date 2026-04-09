using System.Text.Json.Serialization;

namespace WeatherForcast.Infrastructure.Http.Forcast.ProviderContracts;


// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
public class Astro
{
    [JsonPropertyName("sunrise")]
    public string Sunrise { get; set; }

    [JsonPropertyName("sunset")]
    public string Sunset { get; set; }

    [JsonPropertyName("moonrise")]
    public string Moonrise { get; set; }

    [JsonPropertyName("moonset")]
    public string Moonset { get; set; }

    [JsonPropertyName("moon_phase")]
    public string MoonPhase { get; set; }

    [JsonPropertyName("moon_illumination")]
    public int MoonIllumination { get; set; }

    [JsonPropertyName("is_moon_up")]
    public int IsMoonUp { get; set; }

    [JsonPropertyName("is_sun_up")]
    public int IsSunUp { get; set; }
}

public class Condition
{
    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("icon")]
    public string Icon { get; set; }

    [JsonPropertyName("code")]
    public int Code { get; set; }
}

public class Current
{
    [JsonPropertyName("last_updated_epoch")]
    public int LastUpdatedEpoch { get; set; }

    [JsonPropertyName("last_updated")]
    public string LastUpdated { get; set; }

    [JsonPropertyName("temp_c")]
    public decimal TempC { get; set; }

    [JsonPropertyName("temp_f")]
    public decimal TempF { get; set; }

    [JsonPropertyName("is_day")]
    public int IsDay { get; set; }

    [JsonPropertyName("condition")]
    public Condition Condition { get; set; }

    [JsonPropertyName("wind_mph")]
    public decimal WindMph { get; set; }

    [JsonPropertyName("wind_kph")]
    public decimal WindKph { get; set; }

    [JsonPropertyName("wind_degree")]
    public int WindDegree { get; set; }

    [JsonPropertyName("wind_dir")]
    public string WindDir { get; set; }

    [JsonPropertyName("pressure_mb")]
    public decimal PressureMb { get; set; }

    [JsonPropertyName("pressure_in")]
    public decimal PressureIn { get; set; }

    [JsonPropertyName("precip_mm")]
    public decimal PrecipMm { get; set; }

    [JsonPropertyName("precip_in")]
    public decimal PrecipIn { get; set; }

    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }

    [JsonPropertyName("cloud")]
    public int Cloud { get; set; }

    [JsonPropertyName("feelslike_c")]
    public decimal FeelslikeC { get; set; }

    [JsonPropertyName("feelslike_f")]
    public decimal FeelslikeF { get; set; }

    [JsonPropertyName("windchill_c")]
    public decimal WindchillC { get; set; }

    [JsonPropertyName("windchill_f")]
    public decimal WindchillF { get; set; }

    [JsonPropertyName("heatindex_c")]
    public decimal HeatindexC { get; set; }

    [JsonPropertyName("heatindex_f")]
    public decimal HeatindexF { get; set; }

    [JsonPropertyName("dewpoint_c")]
    public decimal DewpointC { get; set; }

    [JsonPropertyName("dewpoint_f")]
    public decimal DewpointF { get; set; }

    [JsonPropertyName("vis_km")]
    public decimal VisKm { get; set; }

    [JsonPropertyName("vis_miles")]
    public decimal VisMiles { get; set; }

    [JsonPropertyName("uv")]
    public decimal Uv { get; set; }

    [JsonPropertyName("gust_mph")]
    public decimal GustMph { get; set; }

    [JsonPropertyName("gust_kph")]
    public decimal GustKph { get; set; }
}

public class Day
{
    [JsonPropertyName("maxtemp_c")]
    public decimal MaxtempC { get; set; }

    [JsonPropertyName("maxtemp_f")]
    public decimal MaxtempF { get; set; }

    [JsonPropertyName("mintemp_c")]
    public decimal MintempC { get; set; }

    [JsonPropertyName("mintemp_f")]
    public decimal MintempF { get; set; }

    [JsonPropertyName("avgtemp_c")]
    public decimal AvgtempC { get; set; }

    [JsonPropertyName("avgtemp_f")]
    public decimal AvgtempF { get; set; }

    [JsonPropertyName("maxwind_mph")]
    public decimal MaxwindMph { get; set; }

    [JsonPropertyName("maxwind_kph")]
    public decimal MaxwindKph { get; set; }

    [JsonPropertyName("totalprecip_mm")]
    public decimal TotalprecipMm { get; set; }

    [JsonPropertyName("totalprecip_in")]
    public decimal TotalprecipIn { get; set; }

    [JsonPropertyName("totalsnow_cm")]
    public decimal TotalsnowCm { get; set; }

    [JsonPropertyName("avgvis_km")]
    public decimal AvgvisKm { get; set; }

    [JsonPropertyName("avgvis_miles")]
    public decimal AvgvisMiles { get; set; }

    [JsonPropertyName("avghumidity")]
    public int Avghumidity { get; set; }

    [JsonPropertyName("daily_will_it_rain")]
    public int DailyWillItRain { get; set; }

    [JsonPropertyName("daily_chance_of_rain")]
    public int DailyChanceOfRain { get; set; }

    [JsonPropertyName("daily_will_it_snow")]
    public int DailyWillItSnow { get; set; }

    [JsonPropertyName("daily_chance_of_snow")]
    public int DailyChanceOfSnow { get; set; }

    [JsonPropertyName("condition")]
    public Condition Condition { get; set; }

    [JsonPropertyName("uv")]
    public decimal Uv { get; set; }
}

public class Forecast
{
    [JsonPropertyName("forecastday")]
    public List<Forecastday> Forecastday { get; set; }
}

public class Forecastday
{
    [JsonPropertyName("date")]
    public string Date { get; set; }

    [JsonPropertyName("date_epoch")]
    public int DateEpoch { get; set; }

    [JsonPropertyName("day")]
    public Day Day { get; set; }

    [JsonPropertyName("astro")]
    public Astro Astro { get; set; }

    [JsonPropertyName("hour")]
    public List<Hour> Hour { get; set; }
}

public class Hour
{
    [JsonPropertyName("time_epoch")]
    public int TimeEpoch { get; set; }

    [JsonPropertyName("time")]
    public string Time { get; set; }

    [JsonPropertyName("temp_c")]
    public decimal TempC { get; set; }

    [JsonPropertyName("temp_f")]
    public decimal TempF { get; set; }

    [JsonPropertyName("is_day")]
    public int IsDay { get; set; }

    [JsonPropertyName("condition")]
    public Condition Condition { get; set; }

    [JsonPropertyName("wind_mph")]
    public decimal WindMph { get; set; }

    [JsonPropertyName("wind_kph")]
    public decimal WindKph { get; set; }

    [JsonPropertyName("wind_degree")]
    public int WindDegree { get; set; }

    [JsonPropertyName("wind_dir")]
    public string WindDir { get; set; }

    [JsonPropertyName("pressure_mb")]
    public decimal PressureMb { get; set; }

    [JsonPropertyName("pressure_in")]
    public decimal PressureIn { get; set; }

    [JsonPropertyName("precip_mm")]
    public decimal PrecipMm { get; set; }

    [JsonPropertyName("precip_in")]
    public decimal PrecipIn { get; set; }

    [JsonPropertyName("snow_cm")]
    public decimal SnowCm { get; set; }

    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }

    [JsonPropertyName("cloud")]
    public int Cloud { get; set; }

    [JsonPropertyName("feelslike_c")]
    public decimal FeelslikeC { get; set; }

    [JsonPropertyName("feelslike_f")]
    public decimal FeelslikeF { get; set; }

    [JsonPropertyName("windchill_c")]
    public decimal WindchillC { get; set; }

    [JsonPropertyName("windchill_f")]
    public decimal WindchillF { get; set; }

    [JsonPropertyName("heatindex_c")]
    public decimal HeatindexC { get; set; }

    [JsonPropertyName("heatindex_f")]
    public decimal HeatindexF { get; set; }

    [JsonPropertyName("dewpoint_c")]
    public decimal DewpointC { get; set; }

    [JsonPropertyName("dewpoint_f")]
    public decimal DewpointF { get; set; }

    [JsonPropertyName("will_it_rain")]
    public int WillItRain { get; set; }

    [JsonPropertyName("chance_of_rain")]
    public int ChanceOfRain { get; set; }

    [JsonPropertyName("will_it_snow")]
    public int WillItSnow { get; set; }

    [JsonPropertyName("chance_of_snow")]
    public int ChanceOfSnow { get; set; }

    [JsonPropertyName("vis_km")]
    public decimal VisKm { get; set; }

    [JsonPropertyName("vis_miles")]
    public decimal VisMiles { get; set; }

    [JsonPropertyName("gust_mph")]
    public decimal GustMph { get; set; }

    [JsonPropertyName("gust_kph")]
    public decimal GustKph { get; set; }

    [JsonPropertyName("uv")]
    public decimal Uv { get; set; }
}

public class Location
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("region")]
    public string Region { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("lat")]
    public decimal Lat { get; set; }

    [JsonPropertyName("lon")]
    public decimal Lon { get; set; }

    [JsonPropertyName("tz_id")]
    public string TzId { get; set; }

    [JsonPropertyName("localtime_epoch")]
    public int LocaltimeEpoch { get; set; }

    [JsonPropertyName("localtime")]
    public string Localtime { get; set; }
}

public class DailyForcastData
{
    [JsonPropertyName("location")]
    public Location Location { get; set; }

    [JsonPropertyName("current")]
    public Current Current { get; set; }

    [JsonPropertyName("forecast")]
    public Forecast Forecast { get; set; }
}

