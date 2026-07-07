using FleetManager.Domain.Entities;
using FleetManager.Domain.Security.Token;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FleetManager.Infrastructure.Security.Token
{
    public class JwtTokenGenerator(uint expirationMinutes, string signingKey, string issuer, string audience) : IAccessTokenGenerator
    {
        private readonly uint _expirationMinutes = expirationMinutes;
        private readonly string _signingKey = signingKey;
        private readonly string _issuer = issuer;
        private readonly string _audience = audience;

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Name),
                new(ClaimTypes.Sid, user.UserIdentifier.ToString()),
                new(ClaimTypes.Role, user.Role),
                new("db_id", user.Id.ToString())

            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_expirationMinutes),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature)
            };


            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);

        }
        private SymmetricSecurityKey SecurityKey()
        {
            var key = Encoding.UTF8.GetBytes(_signingKey);
            return new SymmetricSecurityKey(key);
        }
    }
}
