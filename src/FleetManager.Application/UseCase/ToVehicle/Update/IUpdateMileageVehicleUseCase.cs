using FleetManager.Communication.Request.ToVehicle;

namespace FleetManager.Application.UseCase.ToVehicle.Update
{
    public interface IUpdateMileageVehicleUseCase
    {
        Task Execute(long id, RequestMileageVehicleJson request);
    }
}
