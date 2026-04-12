using FleetManager.communication.Requests;

namespace FleetManager.Application.UseCase.ToVehicle.Update
{
    public interface IUpdateVehicleUseCase
    {
        Task Execute(long id, RequestVehicleJson request);
    }
}
