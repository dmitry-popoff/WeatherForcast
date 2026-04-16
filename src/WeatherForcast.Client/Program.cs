using Blazing.Mvvm;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WeatherForcast.Client;
using WeatherForcast.Client.ViewModels.Forecasts;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMvvm(options =>
{
    options.HostingModelType = BlazorHostingModelType.WebAssembly;
});

builder.Services.Configure<WeatherForecastSettings>(
    builder.Configuration.GetSection(WeatherForecastSettings.Section));

var forcastOptions = WeatherForecastSettings.Default;
builder.Configuration.GetSection(WeatherForecastSettings.Section).Bind(forcastOptions);

builder.Services.AddHttpClient<IWeatherForecastProvider, ForecastHttpClient>(client =>
    client.BaseAddress = new Uri(
        string.IsNullOrWhiteSpace( forcastOptions.BaseAddress)
        ? builder.HostEnvironment.BaseAddress
        : forcastOptions.BaseAddress));

await builder.Build().RunAsync();
