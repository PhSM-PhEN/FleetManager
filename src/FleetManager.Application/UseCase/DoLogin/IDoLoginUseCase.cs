using FleetManager.Communication.Request.ToUser;
using FleetManager.Communication.Response.ToUser;

namespace FleetManager.Application.UseCase.DoLogin
{
    public interface IDoLoginUseCase
    {
        Task<ResponseLoginUserJson> Execute(RequestLoginUserJson request);
    }
}
