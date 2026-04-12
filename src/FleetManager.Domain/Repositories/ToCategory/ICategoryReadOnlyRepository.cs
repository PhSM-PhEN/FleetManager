using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToCategory
{
    public interface ICategoryReadOnlyRepository
    {
        Task<List<Category>> GetAll();

        Task<Category?> GetById(int id);
    }
}
