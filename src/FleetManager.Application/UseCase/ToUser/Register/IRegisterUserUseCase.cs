using FleetManager.communication.Requests.ToUser;
using FleetManager.communication.Responses.ToUsers;

namespace FleetManager.Application.UseCase.ToUser.Register
{
    public interface IRegisterUserUseCase
    {
        Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request);
    }
}
