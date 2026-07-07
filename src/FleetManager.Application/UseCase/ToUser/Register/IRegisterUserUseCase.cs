using FleetManager.Communication.Request.ToUser;
using FleetManager.Communication.Response.ToUser;

namespace FleetManager.Application.UseCase.ToUser.Register
{
    public interface IRegisterUserUseCase
    {
        Task<ResponseLoginUserJson> Execute(RequestRegisterUserJson request);
    }
}
