using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToCompany
{
    public interface ICompanyReadOnlyRepository
    {
        Task<List<Company>> GetAll();
        Task<Company?> GetById(long id);
        Task<bool> ExistByCnpj(string cnpj);
    }
}