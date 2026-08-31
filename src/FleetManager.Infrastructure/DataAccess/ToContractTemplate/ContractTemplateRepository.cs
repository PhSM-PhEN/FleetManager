using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToContractTemplate;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess.ToContractTemplate
{
    internal class ContractTemplateRepository(FleetManagerDbContext dbContext)
        : IContractTemplateWriteOnlyRepository, IContractTemplateReadOnlyRepository
    {
        public async Task Add(ContractTemplate template)
        {
            await dbContext.ContractTemplates.AddAsync(template);
        }

        public async Task<ContractTemplate?> GetById(long id)
        {
            return await dbContext.ContractTemplates
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        async Task<ContractTemplate?> IContractTemplateReadOnlyRepository.GetById(long id)
        {
            return await dbContext.ContractTemplates.AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public void Update(ContractTemplate template)
        {
            dbContext.ContractTemplates.Update(template);
        }

        public async Task<List<ContractTemplate>> GetAllActive()
        {
            return await dbContext.ContractTemplates.AsNoTracking()
                .Where(t => t.IsActive)
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        public async Task<(List<ContractTemplate>, int TotalCount)> GetAll(int pageNumber, int pageSize, bool? onlyActive = null)
        {
            var query = dbContext.ContractTemplates.AsNoTracking();

            if (onlyActive.HasValue)
                query = query.Where(t => t.IsActive == onlyActive.Value);

            var totalCount = await query.CountAsync();
            var templates = await query
                .OrderByDescending(t => t.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (templates, totalCount);
        }
    }
}
