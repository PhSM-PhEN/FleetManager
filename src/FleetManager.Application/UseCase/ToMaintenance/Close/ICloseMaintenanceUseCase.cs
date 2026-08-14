using FleetManager.Communication.Request.ToMaintenance;
using FleetManager.Communication.Response.ToMaintenance;

namespace FleetManager.Application.UseCase.ToMaintenance.Close
{
    public interface ICloseMaintenanceUseCase
    {
        Task<ResponseMaintenanceJson> Execute(long id, RequestClosedMaintenanceJson request);
    }
}
