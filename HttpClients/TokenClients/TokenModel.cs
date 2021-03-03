using System;

namespace HttpClients.TokenCLients
{
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public TimeSpan ExpiresIn { get; set; }
        public string Scheme { get; set; }
    }
}