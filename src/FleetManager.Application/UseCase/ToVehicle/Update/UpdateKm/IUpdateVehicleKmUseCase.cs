using FleetManager.Communication.Requests;
using FleetManager.Communication.Responses;

namespace FleetManager.Application.UseCase.ToVehicle.Update.UpdateKm
{
    public interface IUpdateVehicleKmUseCase
    {
        Task<ResponseVehicleJson> Execute(long id, RequestVehicleUpdateCurrentMileageJson request);
    }
}
