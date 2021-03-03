using Correlation;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClients.Handlers
{
    public class CorrelationIdHttpMessageHandler : DelegatingHandler
    {
        private readonly CorrelationConfiguration _configuration;
        private readonly ICorrelationAccessor _correlationAccessor;

        public CorrelationIdHttpMessageHandler(
            IOptions<CorrelationConfiguration> configuration,
            ICorrelationAccessor correlationAccessor)
        {
            _configuration = configuration.Value;
            _correlationAccessor = correlationAccessor;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add(_configuration.HeaderName, _correlationAccessor.GetId());
            return base.SendAsync(request, cancellationToken);
        }
    }
}
