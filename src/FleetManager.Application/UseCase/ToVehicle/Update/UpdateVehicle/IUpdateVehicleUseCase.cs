using FleetManager.Communication.Requests;

namespace FleetManager.Application.UseCase.ToVehicle.Update.UpdateVehicle
{
    public interface IUpdateVehicleUseCase
    {
        Task Execute(long id, RequestUpdateVehicleJson request);
    }
}
