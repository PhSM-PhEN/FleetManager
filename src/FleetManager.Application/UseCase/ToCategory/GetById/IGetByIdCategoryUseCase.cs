using FleetManager.communication.Resposnes;

namespace FleetManager.Application.UseCase.ToCategory.GetById
{
    public interface IGetByIdCategoryUseCase
    {
        Task<ResponseShortCategoryJson> Execute(int id);
    }
}
