using FleetManager.communication.Resposnes.ToVehicle;

namespace FleetManager.Application.UseCase.ToVehicle.GetAll
{
    public interface IGetAllVehicleUseCase
    {
        Task<ResponseAllVehicleJson> Execute();
    }
}
