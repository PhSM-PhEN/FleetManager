using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Security.Token
{
    public interface IAccesTokenGenerator
    {
        string GenerateToken(User user);
    }
}
