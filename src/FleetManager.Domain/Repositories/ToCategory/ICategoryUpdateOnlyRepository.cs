using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToCategory
{
    public interface ICategoryUpdateOnlyRepository
    {
        void Update(Category category);

        Task<Category?> GetById(int id);
    }
}
