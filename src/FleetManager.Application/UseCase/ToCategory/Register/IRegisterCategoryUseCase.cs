using FleetManager.communication.Requests;
using FleetManager.communication.Responses;

namespace FleetManager.Application.UseCase.ToCategory.Register
{
    public interface IRegisterCategoryUseCase
    {
        Task<ResponseCategoryJson> Execute(RequestCategoryJson request);
    }
}
