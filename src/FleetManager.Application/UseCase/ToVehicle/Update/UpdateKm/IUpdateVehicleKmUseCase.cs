using FleetManager.communication.Requests;
using FleetManager.communication.Resposnes;

namespace FleetManager.Application.UseCase.ToVehicle.Update.UpdateKm
{
    public interface IUpdateVehicleKmUseCase
    {
        Task<ResponseVehicleJson> Execute(long id, RequestVehicleUpdateCurrentMiliageJson request);
    }
}
