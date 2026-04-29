using FleetManager.communication.Requests.ToUser;

namespace FleetManager.Application.UseCase.ToUser.ChangePassword
{
    public interface IChangePasswordUserUseCase
    {
        Task Execute(RequestChangePasswordJson request);
    }
}
