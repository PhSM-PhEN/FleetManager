using FleetManager.Communication.Requests;
using FleetManager.Domain.Entities;

namespace FleetManager.Application.UseCase.ToCategory.Update
{
    public interface IUpdateCategoryUseCase
    {
        Task Execute(long id, RequestCategoryJson request);
    }
}
