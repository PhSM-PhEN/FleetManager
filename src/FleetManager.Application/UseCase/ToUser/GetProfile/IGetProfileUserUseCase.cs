using FleetManager.Communication.Response.ToUser;

namespace FleetManager.Application.UseCase.ToUser.GetProfile
{
    public interface IGetProfileUserUseCase
    {
            Task<ResponseProfileUserJson> Execute();
    }
}
