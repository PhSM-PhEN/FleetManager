using FleetManager.communication.Responses;

namespace FleetManager.Application.UseCase.ToCategory.GetAll
{
    public interface IGetAllCategoyUseCase
    {
        Task<ResponseListCategoryJson> Execute();
    }
}
