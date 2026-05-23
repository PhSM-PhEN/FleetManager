using FleetManager.communication.Responses;

namespace FleetManager.Application.UseCase.ToVehicle.GetAll
{
    public interface IGetAllVehicleUseCase
    {
        Task<ResponseAllVehicleJson> Execute();
    }
}
