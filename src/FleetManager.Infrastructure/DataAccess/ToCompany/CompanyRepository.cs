using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToCompany;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess.ToCompany
{
    public class CompanyRepository(FleetManagerDbContext dbContext) : ICompanyReadOnlyRepository, ICompanyWriteOnlyRepository, ICompanyUpdateOnlyRepository
    {
        

        public async Task Add(Company company)
        {
            await dbContext.Companies.AddAsync(company);
        }

        public async Task Delete(int id)
        {
            var result = await dbContext.Companies.FindAsync(id);
            dbContext.Companies.Remove(result!);
        }

        public async Task<List<Company>> GetAll()
        {
            return await dbContext.Companies.AsNoTracking().ToListAsync();
        }

        public async Task<Company?> GetById(int id)
        {
            return await dbContext.Companies.AsNoTracking().FirstOrDefaultAsync(comp => comp.Id == id);
            
        }
        async Task<Company?> ICompanyUpdateOnlyRepository.GetById(int id)
        {
            return await dbContext.Companies.FirstOrDefaultAsync(comp => comp.Id == id);
        }
        public void Update(Company company)
        {
            dbContext.Companies.Update(company);
        }
    }
}
