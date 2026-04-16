using Blazing.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using WeatherForcast.Models;
using WeatherForcast.Models.Forcast;

namespace WeatherForcast.Client.ViewModels.Forecasts.FetchCurrentWeather;

[ViewModelDefinition(Lifetime = ServiceLifetime.Scoped)]
public sealed partial class FetchCurrentWeatherViewModel : ViewModelBase, IDisposable
{
    private readonly IWeatherForecastProvider _weatherProvider;
    private readonly ILogger<FetchCurrentWeatherViewModel> _logger;

    [ObservableProperty]
    private CurrentWeather? _currentWeather;

    [ObservableProperty]
    private ErrorDetails? _errorDetails;

    public FetchCurrentWeatherViewModel(
        IWeatherForecastProvider weatherProvider,
        ILogger<FetchCurrentWeatherViewModel> logger)
    {
        _weatherProvider = weatherProvider ?? throw new ArgumentNullException(nameof(weatherProvider));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override async Task OnInitializedAsync()
    {
        using CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(20));
        var response = await _weatherProvider.GetCurrentWeather(cts.Token);

        if (response.Value is not null) _currentWeather = response.Value;
        if (response.Error is not null) _errorDetails = response.Error;
    }

    public void Dispose()
    {
        _logger.LogInformation("Disposing {VMName}.", nameof(FetchCurrentWeatherViewModel));
    }
}

