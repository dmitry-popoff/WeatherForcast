using Elastic.Serilog.Sinks;
using Serilog;

namespace WeatherForcast.WebApi.Logging;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
    {
        //builder.Logging.ClearProviders();

        if (builder.Environment.IsDevelopment())
        {
            builder.Host.UseSerilog((context, services, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                //.WriteTo.Console()
                );
        }
        else
        {
            builder.Host.UseSerilog((context, services, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                //.WriteTo.Elasticsearch()
                );
        }
        return builder;
    }
}
