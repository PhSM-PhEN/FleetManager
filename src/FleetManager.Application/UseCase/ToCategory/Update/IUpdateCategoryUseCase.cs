using FleetManager.Communication.Requests;

namespace FleetManager.Application.UseCase.ToCategory.Update
{
    public interface IUpdateCategoryUseCase
    {
        Task Execute(long id, RequestCategoryJson request);
    }
}
