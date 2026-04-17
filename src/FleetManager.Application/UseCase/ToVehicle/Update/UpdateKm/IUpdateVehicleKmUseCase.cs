using FleetManager.communication.Requests.ToVehicle;
using FleetManager.communication.Resposnes.ToVehicle;

namespace FleetManager.Application.UseCase.ToVehicle.Update.UpdateKm
{
    public interface IUpdateVehicleKmUseCase
    {
        Task<ResponseVehicleJson> Execute(long id, RequestVehicleUpdateCurrentMiliageJson request);
    }
}
