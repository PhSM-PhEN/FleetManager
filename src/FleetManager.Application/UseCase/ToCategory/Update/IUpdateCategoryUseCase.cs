using FleetManager.communication.Requests.ToCategory;
using FleetManager.Domain.Entities;

namespace FleetManager.Application.UseCase.ToCategory.Update
{
    public interface IUpdateCategoryUseCase
    {
        Task Execute(int id, RequestCategoryJson request);
    }
}
