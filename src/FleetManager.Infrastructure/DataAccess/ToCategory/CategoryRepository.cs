using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToCategory;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess.ToCategory
{
    internal class CategoryRepository(FleetManagerDbContext dbContext) : ICategoryWriteOnlyRepository, ICategoryReadOnlyRepository, ICategoryUpdateOnlyRepository
    {
        private readonly FleetManagerDbContext _dbContext = dbContext;
        public async Task Add(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
        }

        public async Task Delete(int id)
        {
            var result = await _dbContext.Categories.FindAsync(id);
            _dbContext.Categories.Remove(result!);
        }

        async Task<Category?> ICategoryReadOnlyRepository.GetById(int id)
        {

            return await _dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Category>> GetAll()
        {
            return await _dbContext.Categories.AsNoTracking().ToListAsync();
        }

        public void Update(Category category)
        {
            _dbContext.Categories.Update(category);
        }

        public async Task<Category?> GetById(int id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
