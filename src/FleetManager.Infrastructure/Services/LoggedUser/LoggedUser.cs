using FleetManager.Domain.Entities;
using FleetManager.Domain.Services.LoggedUser;
using FleetManager.Exception.ExceptionBase;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FleetManager.Infrastructure.Services.LoggedUser
{
    internal class LoggedUser(IHttpContextAccessor httpContextAccessor) : ILoggedUser
    {
        public Task<User> Get()
        {
            var claims = httpContextAccessor.HttpContext!.User;

            var identifier = claims.FindFirst(ClaimTypes.Sid)?.Value
                    ?? throw new InvalidOperationException(ResourceErrorMessages.TOKEN_INVALID_OR_MISSING);

            var name = claims.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
            var role = claims.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;

            var dbIdClaim = claims.FindFirst("db_id")?.Value
                    ?? throw new InvalidOperationException(ResourceErrorMessages.TOKEN_INVALID_OR_MISSING);

            if (!long.TryParse(dbIdClaim, out var id))
                throw new InvalidOperationException(ResourceErrorMessages.TOKEN_INVALID_OR_MISSING);

            var user = new User(id, Guid.Parse(identifier), name, role);

            return Task.FromResult(user);
        }
    }
}