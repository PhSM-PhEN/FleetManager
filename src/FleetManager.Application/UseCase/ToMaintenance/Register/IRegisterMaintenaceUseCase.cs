using FleetManager.Communication.Request.ToMaintenace;
using FleetManager.Communication.Response.ToMaintenance;

namespace FleetManager.Application.UseCase.ToMaintenance.Register
{
    public interface IRegisterMaintenaceUseCase
    {
        Task<ResponseRegisterMaintenanceJson> Execute(RequestMaintenanceJson request);
    }
}
