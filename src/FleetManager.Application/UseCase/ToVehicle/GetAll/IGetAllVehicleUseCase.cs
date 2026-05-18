using FleetManager.communication.Responses.ToVehicle;

namespace FleetManager.Application.UseCase.ToVehicle.GetAll
{
    public interface IGetAllVehicleUseCase
    {
        Task<ResponseAllVehicleJson> Execute();
    }
}
