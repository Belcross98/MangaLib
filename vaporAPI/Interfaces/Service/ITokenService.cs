using vaporAPI.Models;

namespace vaporAPI.Interfaces.Service
{
    public interface ITokenService
    {
        string CreateToken(User user);
        string CreateRefreshToken();
    }
}