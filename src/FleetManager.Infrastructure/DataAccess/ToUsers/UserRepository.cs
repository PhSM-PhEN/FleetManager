using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToUser;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess.ToUsers
{
    internal class UserRepository(FleetManagerDbContext context) : IUserReadOnlyRepository, IUserWriteOnlyRepository, IUserUpdateOnlyRepository
    {
        private readonly FleetManagerDbContext _Dbcontext = context;
        public async Task Add(User user)
        {
            await _Dbcontext.Users.AddAsync(user); 
        }

        public async Task Delete(User user)
        {
            var userToDelete = await _Dbcontext.Users.FindAsync(user.Id);
            _Dbcontext.Users.Remove(userToDelete!);
        }

        public async Task<bool> ExistByEmail(string email)
        {
            return await _Dbcontext.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User> GetById(long id)
        {
            return await _Dbcontext.Users.FirstAsync(u => u.Id == id);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _Dbcontext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email.Equals(email));
        }

        public void Update(User user)
        {
            _Dbcontext.Users.Update(user);
        }
    }
}
