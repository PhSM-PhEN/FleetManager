using FleetManager.Domain.Enum;
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToUser;
using FleetManager.Domain.Services.LoggedUser;
using FleetManager.Exception.ExceptionBase;
using Microsoft.EntityFrameworkCore;

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

            try
            {
                await unitOfWork.Commit();
            }
            catch (DbUpdateException)
            {
                // A checagem acima (ExistsByRole) nao e atomica com o commit: duas requisicoes
                // podem passar pela validacao ao mesmo tempo. A garantia real esta na constraint
                // unica do banco (indice UX_Users_SingleAdmin); se ela disparar, e porque outra
                // requisicao venceu a corrida e ja promoveu um Admin nesse meio-tempo.
                throw new ErrorOnValidationException([ResourceErrorMessages.ADMIN_ALREADY_EXISTS]);
            }
        }
    }
}
