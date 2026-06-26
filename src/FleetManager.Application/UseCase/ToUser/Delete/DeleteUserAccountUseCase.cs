using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToUser;
using FleetManager.Domain.Services.LoggedUser;

namespace FleetManager.Application.UseCase.ToUser.Delete
{
    public class DeleteUserAccountUseCase(IUserWriteOnlyRepository repository, ILoggedUser loggedUser, IUnitOfWork unitOfWork) : IDeleteUserAccountUseCase
    {


        public async Task Execute()
        {
            var user = await loggedUser.Get();

            await repository.Delete(user);
            await unitOfWork.Commit();

        }
    }
}
