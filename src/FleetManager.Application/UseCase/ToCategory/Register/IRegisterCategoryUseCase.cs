using FleetManager.communication.Requests.ToCategory;
using FleetManager.communication.Resposnes.ToCategory;

namespace FleetManager.Application.UseCase.ToCategory.Register
{
    public interface IRegisterCategoryUseCase
    {
        Task<ResponseShortCategoryJson> Execute(RequestCategoryJson request);
    }
}
