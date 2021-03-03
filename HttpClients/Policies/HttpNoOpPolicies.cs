using Polly;
using System.Net.Http;

namespace HttpClients.Policies
{
    public static class HttpNoOpPolicies
    {
        public static IAsyncPolicy<HttpResponseMessage> GetHttpNoOpPolicy()
        {
            return Policy.NoOpAsync()
                    .AsAsyncPolicy<HttpResponseMessage>();
        }
    }
}
