using FleetManager.communication.Resposnes;

namespace FleetManager.Application.UseCase.ToCategory.GetAll
{
    public interface IGetAllCategoyUseCase
    {
        Task<ResponseCategoryJson> Execute();
    }
}
