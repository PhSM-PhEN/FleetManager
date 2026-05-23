using FleetManager.communication.Responses;

namespace FleetManager.Application.UseCase.ToCategory.GetById
{
    public interface IGetByIdCategoryUseCase
    {
        Task<ResponseCategoryJson> Execute(int id);
    }
}
