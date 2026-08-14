using FleetManager.Communication.Request.ToMaintenance;
using FleetManager.Communication.Response.ToMaintenance;

namespace FleetManager.Application.UseCase.ToMaintenance.Register
{
    public interface IRegisterMaintenanceUseCase
    {
        Task<ResponseShortMaintenanceJson> Execute(RequestMaintenanceJson request);
    }
}
