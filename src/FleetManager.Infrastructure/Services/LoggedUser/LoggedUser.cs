using FleetManager.Domain.Entities;
using FleetManager.Domain.Security.Token;
using FleetManager.Domain.Services.LoggeUser;
using FleetManager.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FleetManager.Infrastructure.Services.LoggedUser
{
    public class LoggedUser(FleetManagerDbContext dbContext, ITokenProvider tokenProvider) : ILoggedUser
    {
        private readonly FleetManagerDbContext _dbContext = dbContext;
        private readonly ITokenProvider _tokenProvider = tokenProvider;
        public async Task<User> Get()
        {
            string token = _tokenProvider.TokenOnRequest();

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = tokenHandler.ReadJwtToken(token);

            var identifier = jwt.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

            return await _dbContext.Users.AsNoTracking()
                .FirstAsync(user => user.UserIdentifier == Guid.Parse(identifier));
        }
    }
}
