using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToCharge;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess.ToCharge
{
    public class ChargeRepository(FleetManagerDbContext dbContext) : IChargeWriteOnlyRepository, IChargeReadOnlyRepository
    {
        public async Task Add(Charge charge)
        {
            await dbContext.Charges.AddAsync(charge);
        }

        public async Task<List<Charge>> GetByContractId(long contractId)
        {
            return await dbContext.Charges.AsNoTracking()
                .Where(charge => charge.ContractId == contractId)
                .ToListAsync();
        }
    }
}
