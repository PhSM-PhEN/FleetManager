using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToCategory;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess.ToCategory
{
    internal class CategoryRepository(FleetManagerDbContext dbContext) : ICategoryWriteOnlyRepository, ICategoryReadOnlyRepository, ICategoryUpdateOnlyRepository
    {
        
        public async Task Add(Category category)
        {
            await dbContext.Categories.AddAsync(category);
        }

        public async Task Delete(int id)
        {
            var result = await dbContext.Categories.FindAsync(id);
            dbContext.Categories.Remove(result!);
        }

        async Task<Category?> ICategoryReadOnlyRepository.GetById(int id)
        {

            return await dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Category>> GetAll()
        {
            return await dbContext.Categories.AsNoTracking().ToListAsync();
        }

        public void Update(Category category)
        {
            dbContext.Categories.Update(category);
        }

        public async Task<Category?> GetById(int id)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
