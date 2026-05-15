using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToUser
{
    public interface IUserUpdateOnlyRepository
    {
        Task<User> GetById(long id);
        
        void Update(User user);
    }
}
