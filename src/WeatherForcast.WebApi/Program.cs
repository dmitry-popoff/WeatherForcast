using Asp.Versioning;
using Asp.Versioning.Builder;
using Scalar.AspNetCore;
using WeatherForcast.WebApi.Features;
using WeatherForcast.WebApi.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.AddLogging();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version"));
});

builder.Services.AddRateLimiting(builder.Configuration);

builder.Services.AddCache();

builder.Services.AddOpenApi();

builder.Services.AddMiddleware();

builder.Services.AddProblemDetails();

builder.Services.AddHttpClients(builder.Configuration);

builder.Services.AddEndpoints(typeof(Program).Assembly);

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseRateLimiter();

ApiVersionSet apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1))
    .ReportApiVersions()
    .Build();

RouteGroupBuilder versionedGroup = app
    .MapGroup("api/v{version:apiVersion}")
    .WithApiVersionSet(apiVersionSet)
    .RequireRateLimiting("fixed")
    ;

app.MapEndpoints(versionedGroup);

app.Run();
