using FleetManager.Communication.Request.ToUser;

namespace FleetManager.Application.UseCase.ToUser.Update
{
    public interface IUpdateProfileUserUseCase
    {
        Task Execute(RequestUpdateUserJson request);
    }
}
