using FleetManager.Communication.Requests;
using FleetManager.Communication.Responses;

namespace FleetManager.Application.UseCase.ToCategory.Register
{
    public interface IRegisterCategoryUseCase
    {
        Task<ResponseCategoryJson> Execute(RequestCategoryJson request);
    }
}
