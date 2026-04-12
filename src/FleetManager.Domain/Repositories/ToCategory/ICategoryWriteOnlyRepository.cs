using FleetManager.Domain.Entities;

namespace FleetManager.Domain.Repositories.ToCategory
{
    public interface ICategoryWriteOnlyRepository
    {
        Task Add(Category category);

        Task Delete(int id);
    }
}
