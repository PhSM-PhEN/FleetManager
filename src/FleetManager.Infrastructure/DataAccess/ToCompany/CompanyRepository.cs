using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToCompany;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess.ToCompany
{
    internal class CompanyRepository(FleetManagerDbContext dbContext) : ICompanyWriteOnlyRepository, ICompanyReadOnlyRepository
    {
        public async Task Add(Company company)
        {
            await dbContext.Companies.AddAsync(company);
        }

        public Task Delete(Company company)
        {
            dbContext.Companies.Remove(company);
            return Task.CompletedTask;
        }

        public async Task<List<Company>> GetAll()
        {
            return await dbContext.Companies.AsNoTracking()
            .Include(c => c.Address)
            .ToListAsync();
        }

        public async Task<Company?> GetById(long id)
        {
            return await dbContext.Companies.FirstOrDefaultAsync(comp => comp.Id == id);
        }

        public async Task<bool> ExistByCnpj(string cnpj)
        {
            return await dbContext.Companies.AsNoTracking().AnyAsync(c => c.Cnpj == cnpj);
        }

        async Task<Company?> ICompanyReadOnlyRepository.GetById(long id)
        {
            return await dbContext.Companies.AsNoTracking()
           .Include(c => c.Address)
           .FirstOrDefaultAsync(comp => comp.Id == id);
        }
        public void Update(Company company)
        {
            dbContext.Companies.Update(company);
        }
    }
}