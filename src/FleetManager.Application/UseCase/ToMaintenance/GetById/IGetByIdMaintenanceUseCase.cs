using FleetManager.Communication.Response.ToMaintenance;

namespace FleetManager.Application.UseCase.ToMaintenance.GetById
{
    public interface IGetByIdMaintenanceUseCase
    {
        Task<ResposneMaintenanceJson> Execute(long id);
    }
}
