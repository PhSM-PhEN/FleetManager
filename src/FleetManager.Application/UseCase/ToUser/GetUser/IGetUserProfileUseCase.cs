using FleetManager.communication.Resposnes.ToUsers;

namespace FleetManager.Application.UseCase.ToUser.GetUser
{
    public interface IGetUserProfileUseCase
    {
        Task<ResponseUserProfileJson> Execute();
    }
}
