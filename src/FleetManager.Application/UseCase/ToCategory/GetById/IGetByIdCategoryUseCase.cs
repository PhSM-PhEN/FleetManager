using FleetManager.Communication.Responses;

namespace FleetManager.Application.UseCase.ToCategory.GetById
{
    public interface IGetByIdCategoryUseCase
    {
        Task<ResponseCategoryJson> Execute(long id);
    }
}
