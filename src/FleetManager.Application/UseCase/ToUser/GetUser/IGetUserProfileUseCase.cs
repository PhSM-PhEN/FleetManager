using FleetManager.communication.Responses.ToUsers;

namespace FleetManager.Application.UseCase.ToUser.GetUser
{
    public interface IGetUserProfileUseCase
    {
        Task<ResponseUserProfileJson> Execute();
    }
}
