using FleetManager.communication.Requests;

namespace FleetManager.Application.UseCase.ToUser.Update
{
    public interface IUpdateProfileUseCase
    {
        Task Execute(RequestUpdateUserJson request);
    }
}
