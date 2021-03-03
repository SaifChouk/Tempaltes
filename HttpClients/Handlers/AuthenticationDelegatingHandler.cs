using HttpClients.TokenCLients;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClients.Handlers
{
    public class AuthenticationDelegatingHandler : DelegatingHandler
    {
        private readonly ITokenClient _tokenClient;

        public AuthenticationDelegatingHandler(ITokenClient tokenClient)
        {
            _tokenClient = tokenClient;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _tokenClient.GetTokenAsync();
            request.Headers.Authorization = new AuthenticationHeaderValue(token.Scheme, token.AccessToken);

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden)
            {
                token = await _tokenClient.RefreshTokenAsync();
                request.Headers.Authorization = new AuthenticationHeaderValue(token.Scheme, token.AccessToken);
                response = await base.SendAsync(request, cancellationToken);
            }

            return response;
        }
    }
}
