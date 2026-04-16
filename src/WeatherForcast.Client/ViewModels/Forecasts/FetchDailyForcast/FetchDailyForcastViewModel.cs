using Blazing.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using WeatherForcast.Models;
using WeatherForcast.Models.Forcast;

namespace WeatherForcast.Client.ViewModels.Forecasts.FetchCurrentWeather;

[ViewModelDefinition(Lifetime = ServiceLifetime.Scoped)]
public sealed partial class FetchDailyForcastViewModel : ViewModelBase, IDisposable
{
    private readonly IWeatherForecastProvider _weatherProvider;
    private readonly ILogger<FetchDailyForcastViewModel> _logger;
    
    [ObservableProperty]
    private ForcastModel? _forcast;

    [ObservableProperty]
    private ErrorDetails? _errorDetails;

    public string Title => "Daily weather forecast";

    public FetchDailyForcastViewModel(
        IWeatherForecastProvider weatherProvider,
        ILogger<FetchDailyForcastViewModel> logger)
    {
        _weatherProvider = weatherProvider ?? throw new ArgumentNullException(nameof(weatherProvider));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override async Task OnInitializedAsync()
    {
        using CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(20));
        Result<ForcastModel> response = await _weatherProvider.GetDailyForcast(cts.Token);

        if (response.Value is not null) _forcast = response.Value;
        if (response.Error is not null) _errorDetails = response.Error;
    }

    public void Dispose()
    {
        _logger.LogInformation("Disposing {VMName}.", nameof(FetchCurrentWeatherViewModel));
    }
}

