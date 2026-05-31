using FleetManager.Communication.Responses;

namespace FleetManager.Application.UseCase.ToVehicle.GetById
{
    public interface IGetByIdVehicleUseCase
    {
        Task<ResponseVehicleByIdJson> Execute(long id);
    }
}
