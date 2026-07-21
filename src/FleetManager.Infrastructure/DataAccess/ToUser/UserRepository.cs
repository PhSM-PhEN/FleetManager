using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToUser;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess.ToUser
{
    internal class UserRepository(FleetManagerDbContext dbContext) : IUserReadOnlyRepository, IUserWriteOnlyRepository
    {
        public async Task Add(User user)
        {
            await dbContext.Users.AddAsync(user);
        }

        public Task<bool> AnyUserExist()
        {
            return dbContext.Users.AnyAsync();
        }

        public async Task Delete(User user)
        {
            var userToDelete = await dbContext.Users.FindAsync(user.Id);
            dbContext.Users.Remove(userToDelete!);
        }

        public async Task<bool> ExistByEmail(string email)
        {
            return await dbContext.Users.AnyAsync(user => user.Email == email);
        }

        public async Task<bool> ExistsByRole(string role)
        {
            return await dbContext.Users.AnyAsync(user => user.Role == role);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email.Equals(email));
        }

        async Task<User?> IUserReadOnlyRepository.GetUserById(long id)
        {
            return await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Id == id);
        }
        async Task<User?> IUserWriteOnlyRepository.GetUserById(long id)
        {
            return await dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
        }
        public void Update(User user)
        {
            dbContext.Users.Update(user);
        }
    }
}
