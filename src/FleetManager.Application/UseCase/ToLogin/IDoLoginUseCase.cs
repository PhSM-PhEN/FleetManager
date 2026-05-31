using FleetManager.Communication.Requests;
using FleetManager.Communication.Responses;

namespace FleetManager.Application.UseCase.ToLogin
{
    public interface IDoLoginUseCase
    {
        Task<ResponseRegisterUserJson> Execute(RequestLoginUserJson request);
    }
}
