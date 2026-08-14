using FleetManager.Application.Extensions;
using FleetManager.Communication.Response.ToMaintenance;
using FleetManager.Domain.Repositories.ToMaintenance;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToMaintenance.GetById
{
    public class GetByIdMaintenanceUseCase(IMaintenanceReadOnlyRepository repository) : IGetByIdMaintenanceUseCase
    {
        public async Task<ResponseMaintenanceJson> Execute(long id)
        {
            var maintenace = await repository.GetById(id) ?? 
                throw new NotFoundException(ResourceErrorMessages.MAINTENANCE_NOT_FOUND);

            return maintenace.ToInfoResponse();
        }
    }
}
