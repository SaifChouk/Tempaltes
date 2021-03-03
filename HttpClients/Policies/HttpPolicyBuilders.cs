using Polly;
using Polly.Extensions.Http;
using System.Net.Http;

namespace HttpClients.Builders
{
    public static class HttpPolicyBuilders
    {
        public static PolicyBuilder<HttpResponseMessage> GetBaseBuilder()
        {
            return HttpPolicyExtensions.HandleTransientHttpError();
        }

        public static PolicyBuilder<HttpResponseMessage> GetRetryBaseBuilder()
        {

            return HttpPolicyExtensions.HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound);
        }

    }
}
