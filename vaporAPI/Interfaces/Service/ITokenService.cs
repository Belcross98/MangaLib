using vaporAPI.Models;

namespace vaporAPI.Interfaces.Service
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
        string CreateRefreshToken();
    }
}