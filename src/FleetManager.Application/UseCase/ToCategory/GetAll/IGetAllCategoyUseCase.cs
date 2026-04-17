using FleetManager.communication.Resposnes.ToCategory;

namespace FleetManager.Application.UseCase.ToCategory.GetAll
{
    public interface IGetAllCategoyUseCase
    {
        Task<ResponseCategoryJson> Execute();
    }
}
