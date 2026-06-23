using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToUser;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess.ToUsers
{
    internal class UserRepository(FleetManagerDbContext dbContext) : IUserReadOnlyRepository, IUserWriteOnlyRepository, IUserUpdateOnlyRepository
    {
        
        public async Task Add(User user)
        {
            await dbContext.Users.AddAsync(user); 
        }

        public async Task<bool> AnyUserExist()
        {
            return await dbContext.Users.AnyAsync();
        }

        public async Task Delete(User user)
        {
            var userToDelete = await dbContext.Users.FindAsync(user.Id);
            dbContext.Users.Remove(userToDelete!);
        }
        
        public async Task<bool> ExistByEmail(string email)
        {
            return await dbContext.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> ExistByRole(string roles)
        {
            return await dbContext.Users.AnyAsync(u => u.Role == roles);
        }

        public async Task<User> GetById(long id)
        {
            return await dbContext.Users.FirstAsync(u => u.Id == id);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email.Equals(email));
        }

        public async Task<User?> GetUserById(long id)
        {
            return await dbContext.Users.AsNoTracking().FirstAsync(u => u.Id == id);

        }

        public void Update(User user)
        {
            dbContext.Users.Update(user);
        }
    }
}
