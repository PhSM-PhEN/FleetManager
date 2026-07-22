using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToCompany;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess.ToCompany
{
    internal class CompanyRepository(FleetManagerDbContext dbContext) : ICompanyWriteOnlyRepository, ICompanyReadOnlyRepository
    {
        public async Task Add(Company company)
        {
            await dbContext.Companys.AddAsync(company);
        }

        public async Task Delete(long id)
        {
            var result = await dbContext.Companys.FindAsync(id);
            dbContext.Companys.Remove(result!);
        }

        public async Task<List<Company>> GetAll()
        {
            return await dbContext.Companys.AsNoTracking()
            .Include(c => c.Address)
            .ToListAsync();
        }

        public async Task<Company?> GetById(long id)
        {
            return await dbContext.Companys.FirstOrDefaultAsync(comp => comp.Id == id);
        }

        public async Task<bool> ExistByCnpj(string cnpj)
        {
            return await dbContext.Companys.AsNoTracking().AnyAsync(c => c.Cnpj == cnpj);
        }

        async Task<Company?> ICompanyReadOnlyRepository.GetById(long id)
        {
            return await dbContext.Companys.AsNoTracking()
           .Include(c => c.Address)
           .FirstOrDefaultAsync(comp => comp.Id == id);
        }
        public void Update(Company company)
        {
            dbContext.Companys.Update(company);
        }
    }
}