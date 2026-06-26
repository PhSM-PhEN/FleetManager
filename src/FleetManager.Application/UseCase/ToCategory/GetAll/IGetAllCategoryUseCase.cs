using FleetManager.Communication.Responses;

namespace FleetManager.Application.UseCase.ToCategory.GetAll
{
    public interface IGetAllCategoryUseCase
    {
        Task<List<ResponseCategoryJson>> Execute();
    }
}
