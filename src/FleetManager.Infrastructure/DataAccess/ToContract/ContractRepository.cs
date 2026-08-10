using FleetManager.Domain.Entities;
using FleetManager.Domain.Enum;
using FleetManager.Domain.Repositories.ToContract;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess.ToContract
{
    public class ContractRepository(FleetManagerDbContext dbContext) : IContractWriteOnlyRepository, IContractReadOnlyRepository
    {
        public async Task Add(Contract contract)
        {
            await dbContext.Contracts.AddAsync(contract);
        }

        public Task Delete(Contract contract)
        {
            dbContext.Contracts.Remove(contract);
            return Task.CompletedTask;
        }

        public async Task<(List<Contract>, int TotalCount)> GetAll(int pageNumber, int pageSize)
        {
            var query =  dbContext.Contracts.AsNoTracking();
            var totalCount = await query.CountAsync();
            var contracts = await query
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();
            return(contracts, totalCount);            
        }

        public async Task<Contract?> GetById(long id)
        {
            return await dbContext.Contracts
                        .Include(rp => rp.RentalPlan)
                        .FirstOrDefaultAsync(c => c.Id == id);
        }
        async Task<Contract?> IContractReadOnlyRepository.GetById(long id)
        {
            return await dbContext.Contracts.AsNoTracking()
                        .Include(t => t.Tenant)
                            .ThenInclude(t => t.Address)
                        .Include(v => v.Vehicle)
                            .ThenInclude(v => v.Company)
                                .ThenInclude(c => c.Address)
                        .Include(rp => rp.RentalPlan)
                        .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> HasActiveContract(long vehicleId)
        {
            return await dbContext.Contracts.AnyAsync(c => c.VehicleId == vehicleId &&
                ( c.ContractStatus == ContractStatus.Reserved || c.ContractStatus == ContractStatus.Active || c.ContractStatus == ContractStatus.Overdue));
        }

        public void Update(Contract contract)
        {
            dbContext.Contracts.Update(contract);
        }

        public async Task<List<Contract>> GetActiveContractsPastDueDate(DateTime referenceDateTime)
        {
            return await dbContext.Contracts
                .Where(c => c.ContractStatus == ContractStatus.Active && c.ReturnDueDateTime < referenceDateTime)
                .ToListAsync();
        }
    }
}
