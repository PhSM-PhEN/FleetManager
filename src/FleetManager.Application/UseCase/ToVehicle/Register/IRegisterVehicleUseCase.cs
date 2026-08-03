using FleetManager.Communication.Request.ToVehicle;
using FleetManager.Communication.Response.ToVehicle;

namespace FleetManager.Application.UseCase.ToVehicle.Register
{
    public interface IRegisterVehicleUseCase
    {
        Task<ResponseVehicleRegisteredJson> Execute(RequestVehicleJson request);
    }
}
