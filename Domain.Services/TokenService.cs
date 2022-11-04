using Domain.Core.Interfaces.Repositories;
using Domain.Core.Interfaces.Services;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Services
{
    public class TokenService : ITokenService
    {
        private readonly IUserRepository _repository;

        public TokenService(IUserRepository repository)
        {
            this._repository = repository;
        }


        private static string hashToken = "07cc94ce29c48d2cb5f994f8f38f7a2b";

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("07cc94ce29c48d2cb5f994f8f38f7a2b");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(hashToken);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var range = RandomNumberGenerator.Create();
            range.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public static ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(hashToken)),
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtSecuriryToken ||
                    !jwtSecuriryToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        private static List<(string, string)> _refreshTokens = new();

        public void SaveRefreshTokens(string username, string refreshToken)
        {
            _refreshTokens.Add(new(username, refreshToken));
        }
        public string GetRefreshTokens(string username)
        {
            return _refreshTokens.FirstOrDefault(x => x.Item1 == username).Item2;
        }
        public void DeleteRefreshTokens(string username, string refreshToken)
        {
            var item = _refreshTokens.FirstOrDefault(x => x.Item1 == username && x.Item2 == refreshToken);
            _refreshTokens.Remove(item);
        }

        public User? GetUser(string userName, string password)
        {
            return _repository.GetUser(userName, password);
        }
    }
}
