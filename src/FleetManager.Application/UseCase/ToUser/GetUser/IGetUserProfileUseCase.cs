using FleetManager.Communication.Responses;

namespace FleetManager.Application.UseCase.ToUser.GetUser
{
    public interface IGetUserProfileUseCase
    {
        Task<ResponseUserProfileJson> Execute();
    }
}
