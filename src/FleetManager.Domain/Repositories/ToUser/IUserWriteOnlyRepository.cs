using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToUser
{
    public interface IUserWriteOnlyRepository
    {
        Task Add(User user);

        Task Delete(User user);

    }
}
