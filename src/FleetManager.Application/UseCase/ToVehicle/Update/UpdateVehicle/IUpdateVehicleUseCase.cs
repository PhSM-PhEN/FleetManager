using FleetManager.communication.Requests.ToVehicle;

namespace FleetManager.Application.UseCase.ToVehicle.Update.UpdateVehicle
{
    public interface IUpdateVehicleUseCase
    {
        Task Execute(long id, RequestVehicleJson request);
    }
}
