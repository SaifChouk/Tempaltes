using HttpClients.TokenCLients;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System.Net;
using System.Net.Http;

namespace HttpClients.Policies
{
    public static class HttpUnauthorizedPolicy
    {
        public static AsyncRetryPolicy<HttpResponseMessage> GetHttpRetryPolicy(ILogger logger, ITokenClient tokenProvider)
        {

            return Policy.HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.Unauthorized)
            .RetryAsync(1, async (response, retryCount, context) =>
            {
                logger.LogInformation("Request failed with {StatusCode}, Renewing acces token for retry", HttpStatusCode.Unauthorized);
                context["accesToken"] = await tokenProvider.GetTokenAsync();
                logger.LogInformation("Access Token renewd for retry call", HttpStatusCode.Unauthorized);
            });
        }
    }
}
