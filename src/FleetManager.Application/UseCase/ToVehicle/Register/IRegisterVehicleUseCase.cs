using FleetManager.Communication.Requests;
using FleetManager.Communication.Responses;

namespace FleetManager.Application.UseCase.ToVehicle.Register
{
    public interface IRegisterVehicleUseCase
    {
        Task<ResponseRegisterVehicleJson> Execute(RequestVehicleJson request);
    }
}
