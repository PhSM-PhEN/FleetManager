using FleetManager.Domain.Entities;
using FleetManager.Domain.Services.LoggedUser;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FleetManager.Infrastructure.Services.LoggedUser
{
    public class LoggedUser(IHttpContextAccessor httpContextAccessor) : ILoggedUser
    {
        public Task<User> Get()
        {
            var claims = httpContextAccessor.HttpContext!.User;

            var identifier = claims.FindFirst(ClaimTypes.Sid)?.Value
                ?? throw new UnauthorizedAccessException("Token inválido ou ausente.");

            var name = claims.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
            var role = claims.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;

            var user = new User(Guid.Parse(identifier), name, role);

            return Task.FromResult(user);
        }
    }
}