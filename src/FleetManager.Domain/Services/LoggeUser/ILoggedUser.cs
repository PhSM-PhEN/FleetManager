using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Services.LoggeUser
{
    public interface ILoggedUser
    {
        Task<User> Get();
    }
}
