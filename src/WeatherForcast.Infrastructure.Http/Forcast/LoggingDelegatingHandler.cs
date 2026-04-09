using Microsoft.Extensions.Logging;

namespace WeatherForcast.Infrastructure.Http.Forcast;

public class LoggingDelegatingHandler(ILogger<LoggingDelegatingHandler> logger)
    : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Before HTTP request");

            var result = await base.SendAsync(request, cancellationToken);

            logger.LogInformation($"After HTTP request: {result.StatusCode.ToString()}");

            return result;
        }
        catch (Exception e)
        {
            logger.LogError(e, "HTTP request failed");

            throw;
        }
    }
}

//public class RetryDelegatingHandler : DelegatingHandler
//{
//    private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy =
//        Policy<HttpResponseMessage>
//            .Handle<HttpRequestException>()
//            .RetryAsync(2);

//    protected override async Task<HttpResponseMessage> SendAsync(
//        HttpRequestMessage request,
//        CancellationToken cancellationToken)
//    {
//        var policyResult = await _retryPolicy.ExecuteAndCaptureAsync(
//            () => base.SendAsync(request, cancellationToken));

//        if (policyResult.Outcome == OutcomeType.Failure)
//        {
//            throw new HttpRequestException(
//                "Something went wrong",
//                policyResult.FinalException);
//        }

//        return policyResult.Result;
//    }
//}
