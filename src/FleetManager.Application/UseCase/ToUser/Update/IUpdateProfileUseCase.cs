using FleetManager.Communication.Requests;

namespace FleetManager.Application.UseCase.ToUser.Update
{
    public interface IUpdateProfileUseCase
    {
        Task Execute(RequestUpdateUserJson request);
    }
}
