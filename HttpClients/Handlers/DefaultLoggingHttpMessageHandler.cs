using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClients.Handlers
{
    public class DefaultLoggingHttpMessageHandler : DelegatingHandler
    {
        private readonly ILogger<DefaultLoggingHttpMessageHandler> _logger;

        public DefaultLoggingHttpMessageHandler(ILogger<DefaultLoggingHttpMessageHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));


        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            try
            {
                _logger.LogInformation("Start processing HTTP request {HttpMethod} {Uri}", request.Method, request.RequestUri);

                var response = await base.SendAsync(request, cancellationToken);

                if (response.IsSuccessStatusCode)
                    _logger.LogInformation("End processing HTTP request {HttpMethod} {Uri} - {StatusCode} ", request.Method, request.RequestUri, response.StatusCode);

                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Fail processing HTTP request {HttpMethod} {Uri}", request.Method, request.RequestUri);
                throw;
            }

        }

    }
}
