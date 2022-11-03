using Domain.Entities;

namespace Domain.Core.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        string GenerateRefreshToken();
        void SaveRefreshTokens(string username, string refreshToken);
        void DeleteRefreshTokens(string username, string refreshToken);
        User? GetUser(string userName, string password);
    }
}
