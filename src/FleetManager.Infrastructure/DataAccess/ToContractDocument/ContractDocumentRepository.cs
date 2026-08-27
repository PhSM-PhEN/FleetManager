using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToContractDocument;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess.ToContractDocument
{
    internal class ContractDocumentRepository(FleetManagerDbContext dbContext) : IContractDocumentWriteOnlyRepository
    {
        public async Task Add(ContractDocument document)
        {
            await dbContext.ContractDocuments.AddAsync(document);
        }

        public async Task<ContractDocument?> GetByContractId(long contractId)
        {
            return await dbContext.ContractDocuments.AsNoTracking()
                .Where(d => d.ContractId == contractId)
                .OrderByDescending(d => d.GeneratedAt)
                .FirstOrDefaultAsync();
        }
    }
}
