using FleetManager.communication.Requests;

namespace FleetManager.Application.UseCase.ToUser.ChangePassword
{
    public interface IChangePasswordUserUseCase
    {
        Task Execute(RequestChangePasswordJson request);
    }
}
