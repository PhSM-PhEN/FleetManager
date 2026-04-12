using FleetManager.communication.Resposnes;

namespace FleetManager.Application.UseCase.ToVehicle.GetAll
{
    public interface IGetAllVehicleUseCase
    {
        Task<ResponseAllVehicleJson> Execute();
    }
}
