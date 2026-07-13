using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToUser;
using FleetManager.Domain.Services.LoggedUser;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToUser.Delete
{
    public class DeleteUserUseCase(ILoggedUser logged, IUserWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IDeleteUserUseCase
    {
        public async Task Execute()
        {
            var loggedUser = await logged.Get();

            var user = await repository.GetUserById(loggedUser.Id);

            if (user == null) {
                throw new NotFoundException(ResourceErrorMessages.USER_NOT_FOUND);
            }
            await repository.Delete(user);
            await unitOfWork.Commit();
        }
    }
}