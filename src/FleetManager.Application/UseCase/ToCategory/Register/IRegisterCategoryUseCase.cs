using FleetManager.communication.Requests;
using FleetManager.communication.Resposnes;

namespace FleetManager.Application.UseCase.ToCategory.Register
{
    public interface IRegisterCategoryUseCase
    {
        Task<ResponseShortCategoryJson> Execute(RequestCategoryJson request);
    }
}
