using FleetManager.communication.Requests.ToUser;

namespace FleetManager.Application.UseCase.ToUser.Update
{
    public interface IUpdateProfileUseCase
    {
        Task Execute(RequestUpdateUserJson request);
    }
}
