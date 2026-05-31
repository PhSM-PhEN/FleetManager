using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Services.LoggedUser
{
    public interface ILoggedUser
    {
        Task<User> Get();
    }
}
