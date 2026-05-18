using FleetManager.communication.Responses.ToCategory;

namespace FleetManager.Application.UseCase.ToCategory.GetById
{
    public interface IGetByIdCategoryUseCase
    {
        Task<ResponseShortCategoryJson> Execute(int id);
    }
}
