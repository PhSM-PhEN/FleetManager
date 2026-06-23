using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToUser
{
    public interface IUserReadOnlyRepository
    {
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserById(long id);
        Task<bool> AnyUserExist();
        Task<bool> ExistByEmail(string email);
        Task<bool> ExistByRole(string roles);
    }
}
