using FleetManager.communication.Requests;
using FleetManager.communication.Responses;

namespace FleetManager.Application.UseCase.ToVehicle.Register
{
    public interface IRegisterVehicleUseCase
    {
        Task<ResponseRegisterVehicleJson> Execute(RequestVehicleJson request);
    }
}
