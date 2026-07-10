using FleetManager.Communication.Request.ToUser;
using FleetManager.Domain.Repositories.ToUser;
using FleetManager.Domain.Services.LoggedUser;

namespace FleetManager.Application.UseCase.ToUser.Update
{
    public class UpdateProfileUserUseCase(IUserWriteOnlyRepository repository, ILoggedUser logged) : IUpdateProfileUserUseCase
    {
        public async Task Execute(RequestUpdateUserJson request)
        {
            var loggedUser = await logged.Get();

            var user = await repository.GetUserById(loggedUser.Id);
        }
        private async Task Validate(RequestUpdateUserJson request)
        {

        }
    }
    
}
