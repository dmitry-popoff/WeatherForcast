using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Options;
using System.Net;
using WeatherForcast.Infrastructure.Http.Forcast;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddForcastClient(configuration);
    }

    internal static IServiceCollection AddForcastClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ForcastSettings>(configuration.GetSection(ForcastSettings.Section));

        services.AddTransient<LoggingDelegatingHandler>();

        services
            .AddHttpClient<IForcastClient, ForcastClient>((serviceProvider, client) =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<ForcastSettings>>().Value;

                client.BaseAddress = new Uri(settings.Host);
                client.Timeout = TimeSpan.FromSeconds(20);
                client.DefaultRequestHeaders.Add("Accept", "*/*");
                client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                client.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.45.0");
                client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
                client.DefaultRequestVersion = HttpVersion.Version11;
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            //.AddHttpMessageHandler<LoggingDelegatingHandler>()
            .AddStandardResilienceHandler(options =>
            {
                options.Retry.DisableFor(HttpMethod.Get, HttpMethod.Post, HttpMethod.Delete);
                options.TotalRequestTimeout.Timeout = TimeSpan.FromSeconds(20);
            })
            ;

        return services;
    }
}


