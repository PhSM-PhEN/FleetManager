using FleetManager.Communication.Requests;
using FleetManager.Communication.Responses;

namespace FleetManager.Application.UseCase.ToUser.Register
{
    public interface IRegisterUserUseCase
    {
        Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request);
    }
}
