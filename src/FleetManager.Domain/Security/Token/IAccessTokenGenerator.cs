using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Security.Token
{
    public interface IAccessTokenGenerator
    {
        string GenerateToken(User user);
    }
}
