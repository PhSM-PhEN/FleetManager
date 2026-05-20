using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToCompany;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Infrastructure.DataAccess.ToCompany
{
    public class CompanyRepository(FleetManagerDbContext dbContext) : ICompanyReadOnlyRepository, ICompanyWriteOnlyRepository, ICompanyUpdateOnlyRepository
    {
        private readonly FleetManagerDbContext _dbcontext = dbContext;

        public async Task Add(Company company)
        {
            await _dbcontext.Companies.AddAsync(company);
        }

        public async Task Delete(int id)
        {
            var result = await _dbcontext.Companies.FindAsync(id);
            _dbcontext.Companies.Remove(result!);
        }

        public async Task<List<Company>> GetAll()
        {
            return await _dbcontext.Companies.AsNoTracking().ToListAsync();
        }

        public async Task<Company?> GetById(int id)
        {
            return await _dbcontext.Companies.AsNoTracking().FirstOrDefaultAsync(comp => comp.Id == id);
            
        }
        async Task<Company?> ICompanyUpdateOnlyRepository.GetById(int id)
        {
            return await _dbcontext.Companies.FirstOrDefaultAsync(comp => comp.Id == id);
        }
        public void Update(Company company)
        {
            _dbcontext.Companies.Update(company);
        }
    }
}
