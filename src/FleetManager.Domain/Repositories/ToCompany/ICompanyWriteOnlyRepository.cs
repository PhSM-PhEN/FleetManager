using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToCompany
{
    public interface ICompanyWriteOnlyRepository
    {
        Task Add(Company company);
        Task Delete(long id);
        Task<Company?> GetById(long id);
        void Update(Company company);

    }
}
