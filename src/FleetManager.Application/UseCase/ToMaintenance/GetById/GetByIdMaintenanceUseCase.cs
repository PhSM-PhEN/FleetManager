using FleetManager.Application.Extensions;
using FleetManager.Communication.Response.ToMaintenance;
using FleetManager.Domain.Repositories.ToMaintenance;
using FleetManager.Exception.ExceptionBase;

namespace FleetManager.Application.UseCase.ToMaintenance.GetById
{
    public class GetByIdMaintenanceUseCase(IMaintenanceReadOnlyRepository repository) : IGetByIdMaintenanceUseCase
    {
        public async Task<ResposneMaintenanceJson> Execute(long id)
        {
            var maintenace = await repository.GetById(id) ?? 
                throw new NotFoundException("manutançao nao encontrada");

            return maintenace.ToInfoResponse();
        }
    }
}
