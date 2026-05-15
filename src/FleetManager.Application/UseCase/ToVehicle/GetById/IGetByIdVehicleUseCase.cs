using FleetManager.communication.Resposnes.ToVehicle;

namespace FleetManager.Application.UseCase.ToVehicle.GetById
{
    public interface IGetByIdVehicleUseCase
    {
        Task<ResponseVehicleByIdJson> Execute(long id);
    }
}
