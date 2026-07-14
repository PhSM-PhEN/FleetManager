using FleetManager.Domain.Enum;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToUser;
using FleetManager.Domain.Services.LoggedUser;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToUser.Promote
{
    public class PromoteUserUseCase(IUserWriteOnlyRepository repository, IUserReadOnlyRepository readRepository, ILoggedUser logged, IUnitOfWork unitOfWork) : IPromoteUserUseCase
    {
        public async Task Execute()
        {
            var adminExists = await readRepository.ExistsByRole(Roles.ADMIN);

            if (adminExists)
            {
                throw new ErrorOnValidationException([ResourceErrorMessages.ADMIN_ALREADY_EXISTS]);
            }
            var loggedUser = await logged.Get();

            var user = await repository.GetUserById(loggedUser.Id)
                            ?? throw new NotFoundException(ResourceErrorMessages.USER_NOT_FOUND);
            user.PromoteToAdmin();
            repository.Update(user);
            await unitOfWork.Commit();
        }
    }
}
