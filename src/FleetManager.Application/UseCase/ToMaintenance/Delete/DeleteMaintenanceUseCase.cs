
using FleetManager.Domain.Repositories;
using FleetManager.Domain.Repositories.ToMaintenance;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToMaintenance.Delete
{
    public class DeleteMaintenanceUseCase(
        IMaintenanceWriteOnlyRepository repository,
        IUnitOfWork unitOfWork
    ) : IDeleteMaintenanceUseCase
    {
        public async Task Execute(long id)
        {
            var maintenance = await repository.GetById(id) ??
                throw new NotFoundException(ResourceErrorMessages.MAINTENANCE_NOT_FOUND);
            await repository.Delete(maintenance);
            await unitOfWork.Commit();


            /// tem que deixar um filtro para não deletar manutenções que já foram fechadas, ou seja, que já foram realizadas.
            /// implementaçao nao trava os demais passou entao lembrar de corrigir isso depois, pois nao faz sentido deletar uma manutenção que já foi realizada, pois ela é um registro histórico do veiculo.
        }
    }
}
