using FleetManager.communication.Requests.ToVehicle;
using FleetManager.communication.Resposnes.ToVehicle;

namespace FleetManager.Application.UseCase.ToVehicle.Register
{
    public interface IRegisterVehicleUseCase
    {
        Task<ResponseRegisterVehicleJson> Execute(RequestVehicleJson request);
    }
}
