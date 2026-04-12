using FleetManager.communication.Requests;
using FleetManager.communication.Resposnes;

namespace FleetManager.Application.UseCase.ToVehicle.Register
{
    public interface IRegisterVehicleUseCase
    {
        Task<ResponseRegisterVehicleJson> Execute(RequestVehicleJson request);
    }
}
