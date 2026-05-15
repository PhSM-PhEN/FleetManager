using FleetManager.communication.Requests.ToLogin;
using FleetManager.communication.Resposnes.ToUsers;

namespace FleetManager.Application.UseCase.ToLogin
{
    public interface IDoLoginUseCase
    {
        Task<ResponseRegisterUserJson> Execute(RequestLoginUserJson request);
    }
}
