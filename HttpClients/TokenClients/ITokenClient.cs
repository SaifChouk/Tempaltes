using System.Threading.Tasks;

namespace HttpClients.TokenCLients
{
    public interface ITokenClient
    {
        Task<TokenModel> GetTokenAsync();
        Task<TokenModel> RefreshTokenAsync();
    }
}