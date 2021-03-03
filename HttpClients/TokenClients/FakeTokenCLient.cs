using HttpClients.TokenCLients;
using System;
using System.Net;
using System.Threading.Tasks;

namespace HttpClients.TokenClients
{
    public class FakeTokenCLient : ITokenClient
    {
        public Task<TokenModel> GetTokenAsync()
        {
            return Task.FromResult(new TokenModel
            {
                AccessToken = "invalid-token",
                Scheme = "bearer",
                ExpiresIn = new TimeSpan(0, 0, 20)
            });
        }

        public Task<TokenModel> RefreshTokenAsync()
        {
            return Task.FromResult(new TokenModel
            {
                AccessToken = "valid-token",
                Scheme = "bearer",
                ExpiresIn = new TimeSpan(1, 0, 0)
            });
        }
    }
}
