using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToUser
{
    public interface IUserReadOnlyRepository
    {
        Task<User?> GetUserByEmail(string email);

        Task<bool> ExistByEmail(string email);
    }
}
