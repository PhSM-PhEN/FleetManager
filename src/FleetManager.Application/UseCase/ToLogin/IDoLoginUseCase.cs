using FleetManager.communication.Requests;
using FleetManager.communication.Responses;

namespace FleetManager.Application.UseCase.ToLogin
{
    public interface IDoLoginUseCase
    {
        Task<ResponseRegisterUserJson> Execute(RequestLoginUserJson request);
    }
}
