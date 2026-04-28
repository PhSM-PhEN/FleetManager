using FleetManager.Domain.Entities;
using FleetManager.Domain.Security.Token;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FleetManager.Infrastructure.Security.Token
{
    public class JwtTokenGenerator(uint expirationTimeInMinutes, string signingKey) : IAccesTokenGenerator
    {
        private readonly uint _expirationTimeInMinutes = expirationTimeInMinutes;
        private readonly string _signingKey = signingKey;

        public string GenerateToken(User user)
        {

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Name),
                new(ClaimTypes.Sid, user.UserIdentifier.ToString()),
                new(ClaimTypes.Role, user.Role),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_expirationTimeInMinutes),
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

