
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
        }
    }
}
