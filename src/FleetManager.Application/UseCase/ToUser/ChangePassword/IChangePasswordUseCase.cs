using FleetManager.Communication.Request.ToUser;

namespace FleetManager.Application.UseCase.ToUser.ChangePassword
{
    public interface IChangePasswordUseCase
    {
        Task Execute(RequestChangPasswordJson request);
    }
}
