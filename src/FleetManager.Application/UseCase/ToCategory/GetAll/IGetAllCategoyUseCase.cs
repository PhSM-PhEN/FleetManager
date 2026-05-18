using FleetManager.communication.Responses.ToCategory;

namespace FleetManager.Application.UseCase.ToCategory.GetAll
{
    public interface IGetAllCategoyUseCase
    {
        Task<ResponseCategoryJson> Execute();
    }
}
