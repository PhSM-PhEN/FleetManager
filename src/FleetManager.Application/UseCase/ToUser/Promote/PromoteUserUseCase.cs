using FleetManager.Domain.DomainExceptionBase;
using FleetManager.Domain.Enums;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToUser;
using FleetManager.Domain.Services.LoggedUser;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToUser.Promote
{
    public class PromoteUserUseCase(IUserReadOnlyRepository userReadOnly,
    IUserUpdateOnlyRepository updateRepository, IUnitOfWork unitOfWork, ILoggedUser loggedUser) : IPromoteUserUseCase
    {
        public async Task Execute()
        {
            var adminExists = await userReadOnly.ExistByRole(Roles.ADMIN);
            if (adminExists)
                throw new DomainRuleException(ResourceMessages.ADMIN_ALREADY_EXISTS);

            var logged = await loggedUser.Get();

            var user = await updateRepository.GetById(logged.Id)
                    ?? throw new NotFoundException(ResourceMessages.USER_NOT_FOUND);

            user.PromoteToAdmin();
            updateRepository.Update(user);
            await unitOfWork.Commit();
        }
    }
}
