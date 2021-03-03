using HttpClients.TokenCLients;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients.UnitTests
{
    public class TestTokenClient : ITokenClient
    {
        public Task<TokenModel> GetTokenAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TokenModel> RefreshTokenAsync()
        {
            throw new NotImplementedException();
        }
    }
}
