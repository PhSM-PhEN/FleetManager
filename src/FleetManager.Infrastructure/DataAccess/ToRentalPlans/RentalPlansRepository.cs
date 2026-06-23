using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToRentalPlans;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess.ToRentalPlans
{
    public class RentalPlansRepository(FleetManagerDbContext dbContext) : IRentalPlansReadOnlyRepository, 
    IRentalPlansUpdateOnlyRepository,
    IRentalPlansWriteOnlyRepository
    {
        
        public async Task Add(RentalPlan rentalPlan)
        {
            await dbContext.RentalPlans.AddAsync(rentalPlan);
        }

        public async Task Delete(long id)
        {
            var rentalPlan = await dbContext.RentalPlans.FindAsync(id);
            if (rentalPlan != null)
            {
                dbContext.RentalPlans.Remove(rentalPlan);
            }
        }

        public async Task<List<RentalPlan>> GetAll()
        {
            return await dbContext.RentalPlans.AsNoTracking().ToListAsync();
        }

        public async Task<RentalPlan?> GetById(long id)
        {
            return await dbContext.RentalPlans.AsNoTracking().FirstOrDefaultAsync(rp => rp.Id == id);
        }
        async Task<RentalPlan?> IRentalPlansUpdateOnlyRepository.GetById(long id)
        {
            return await dbContext.RentalPlans.FirstOrDefaultAsync(rp => rp.Id == id);
        }
        

            
        

        public void Update(RentalPlan rentalPlan)
        {
            dbContext.RentalPlans.Update(rentalPlan);
        }

       
    }
}
