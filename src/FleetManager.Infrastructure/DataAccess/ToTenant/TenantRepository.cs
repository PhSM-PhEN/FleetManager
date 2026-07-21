using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToTenant;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess.ToTenant
{
    internal class TenantRepository(FleetManagerDbContext dbContext) : ITenantWriteOnlyRepository, ITenanteReadOnlyRepository
    {
        public async Task Add(Tenant tenant)
        {
            await dbContext.Tenants.AddAsync(tenant);

        }

        public async Task Delete(long id)
        {
            var tenant = await GetById(id);
            dbContext.Tenants.Remove(tenant!);
        }

        public async Task<(List<Tenant>, int totalCount)> GetAll(int pageNumber, int pageSize)
        {
            var query = dbContext.Tenants.AsNoTracking();
            var totalCount = await query.CountAsync();
            var tenant = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (tenant, totalCount);
        }

        public async Task<Tenant?> GetById(long id)
        {
            return await dbContext.Tenants.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> ExistByCpf(string cpf)
        {
            return await dbContext.Tenants.AsNoTracking().AnyAsync(t => t.Cpf.Number == cpf);
        }

        public void Update(Tenant tenant)
        {
            dbContext.Tenants.Update(tenant);
        }

        async Task<Tenant?> ITenanteReadOnlyRepository.GetById(long id)
        {
            return await dbContext.Tenants.AsNoTracking()
            .Include(t => t.Address)
            .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}