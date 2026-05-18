using FleetManager.communication.Responses.ToVehicle;

namespace FleetManager.Application.UseCase.ToVehicle.GetById
{
    public interface IGetByIdVehicleUseCase
    {
        Task<ResponseVehicleByIdJson> Execute(long id);
    }
}
