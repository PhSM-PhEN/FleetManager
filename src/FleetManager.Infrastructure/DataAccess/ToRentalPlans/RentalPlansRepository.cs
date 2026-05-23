using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToRentalPlans;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess.ToRentalPlans
{
    public class RentalPlansRepository(FleetManagerDbContext dbContext) : IRentalPlansReadOnlyRepository, IRentalPlansUpdateOnlyRepository, IRentalPlansWriteOnlyRepository
    {
        private readonly FleetManagerDbContext _dbContext = dbContext;
        public async Task Add(RentalPlan rentalPlan)
        {
            await _dbContext.RentalPlans.AddAsync(rentalPlan);
        }

        public async Task Delete(int id)
        {
            var rentalPlan = await _dbContext.RentalPlans.FindAsync(id);
            if (rentalPlan != null)
            {
                _dbContext.RentalPlans.Remove(rentalPlan);
            }
        }

        public async Task<List<RentalPlan>> GetAll()
        {
            return await _dbContext.RentalPlans.AsNoTracking().ToListAsync();
        }

        public async Task<RentalPlan?> GetById(long id)
        {
            return await _dbContext.RentalPlans.AsNoTracking().FirstOrDefaultAsync(rp => rp.Id == id);
        }
        async Task<RentalPlan?> IRentalPlansUpdateOnlyRepository.GetById(int id)
        {
            return await _dbContext.RentalPlans.FirstOrDefaultAsync(rp => rp.Id == id);
        }

        public void Update(RentalPlan rentalPlan)
        {
            _dbContext.RentalPlans.Update(rentalPlan);
        }
    }
}
